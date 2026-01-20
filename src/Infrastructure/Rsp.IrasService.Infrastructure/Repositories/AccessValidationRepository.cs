using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Constants;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.Repositories;

public class AccessValidationRepository(IrasContext irasContext) : IAccessValidationRepository
{
    /// <summary>
    /// Returns true if any of the access paths from the SQL exist for the given user and optional ids.
    /// The method checks in order: applicant (project record owner), sponsor organisation project access.
    /// Short-circuits when any path grants access.
    /// </summary>
    public async Task<bool> HasProjectAccess(string userId, string projectRecordId)
    {
        var projectRecord = await irasContext.ProjectRecords
            .AsNoTracking()
            .FirstOrDefaultAsync(pr => pr.Id == projectRecordId && pr.IsActive);

        // 1. Applicant: if we have project record check if project belongs to the user
        if (projectRecord != null && projectRecord.UserId == userId)
        {
            return true;
        }

        var userGuid = Guid.TryParse(userId, out var guid) ? guid : Guid.Empty;

        // 2. Sponsor indirect access to ProjectRecord via SponsorOrganisation (project-level)
        if (projectRecord?.Status is ProjectRecordStatus.Active or ProjectRecordStatus.PendingClosure or ProjectRecordStatus.Closed)
        {
            if (await SponsorHasAccessToProjectRecord(userGuid, projectRecord.Id))
            {
                return true;
            }
        }

        // No access path found
        return false;
    }

    /// <summary>
    /// Returns true if any of the access paths from the SQL exist for the given user and optional ids.
    /// The method checks in order: applicant (project record owner), modification sponsor, modification reviewer.
    /// Short-circuits when any path grants access.
    /// </summary>
    public async Task<bool> HasModificationAccess(string userId, string? projectRecordId, Guid modificationId)
    {
        var projectRecord = default(ProjectRecord);
        var modification = default(ProjectModification);

        if (modificationId != Guid.Empty)
        {
            modification = await irasContext.ProjectModifications
                .AsNoTracking()
                .FirstOrDefaultAsync(pm => pm.Id == modificationId);

            // if we have the modification, but no projectRecordId, set it from modification
            if (modification != null && string.IsNullOrEmpty(projectRecordId))
            {
                projectRecordId = modification.ProjectRecordId;
            }
        }

        if (!string.IsNullOrEmpty(projectRecordId))
        {
            projectRecord = await irasContext.ProjectRecords
                .AsNoTracking()
                .FirstOrDefaultAsync(pr => pr.Id == projectRecordId && pr.IsActive);
        }

        // 1. Applicant: if we have project record and modification, check if the modification belongs to the project record
        // user is the creator of the project record
        if
        (
            projectRecord != null &&
            modification != null &&
            modification.ProjectRecordId == projectRecord.Id &&
            projectRecord.UserId == userId
        )
        {
            return true;
        }

        // 2. Applicant: if we have project record, check if the user is the creator (UserId) of the project record
        if (projectRecord != null && projectRecord.UserId == userId)
        {
            return true;
        }

        var userGuid = Guid.TryParse(userId, out var guid) ? guid : Guid.Empty;

        // If modification id provided, check modification-level access
        if (modification != null && !string.IsNullOrWhiteSpace(modification.Status) &&
            modification.Status is
                ModificationStatus.WithSponsor or
                ModificationStatus.WithReviewBody or
                ModificationStatus.Approved or
                ModificationStatus.NotAuthorised or
                ModificationStatus.NotApproved
            )
        {
            // read sponsor organisation id from project answers (IQA0312)
            if (await SponsorHasAccessToProjectRecord(userGuid, modification.ProjectRecordId))
            {
                return true;
            }

            // 3. Reviewer access for modification: status 'With review body' onwards and ReviewerId matches userId
            if (modification.Status is not ModificationStatus.WithSponsor)
            {
                if (!string.IsNullOrWhiteSpace(modification.ReviewerId) && string.Equals(modification.ReviewerId, userId, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
        }

        // 4. Sponsor indirect access to ProjectRecord via SponsorOrganisation (project-level)
        if (modificationId == Guid.Empty && (projectRecord?.Status is ProjectRecordStatus.Active or ProjectRecordStatus.PendingClosure or ProjectRecordStatus.Closed))
        {
            if (await SponsorHasAccessToProjectRecord(userGuid, projectRecord.Id))
            {
                return true;
            }
        }

        // No access path found
        return false;
    }

    /// <summary>
    /// Returns true if any of the access paths from the SQL exist for the given user and optional ids.
    /// The method checks in order: applicant (project record owner), modification sponsor, modification reviewer.
    /// Short-circuits when any path grants access.
    /// </summary>
    public async Task<bool> HasModificationAccess(string userId, Guid modificationChangeId)
    {
        var projectRecord = default(ProjectRecord);
        var modification = default(ProjectModification);
        var modificationChange = default(ProjectModificationChange);

        string? projectRecordId = null;

        if (modificationChangeId != Guid.Empty)
        {
            modificationChange = await irasContext.ProjectModificationChanges
                .AsNoTracking()
                .FirstOrDefaultAsync(pm => pm.Id == modificationChangeId);

            if (modificationChange != null)
            {
                modification = await irasContext.ProjectModifications
                    .AsNoTracking()
                    .SingleAsync(pm => pm.Id == modificationChange.ProjectModificationId);

                projectRecordId = modification.ProjectRecordId;
            }
        }

        if (!string.IsNullOrEmpty(projectRecordId))
        {
            projectRecord = await irasContext.ProjectRecords
                .AsNoTracking()
                .FirstOrDefaultAsync(pr => pr.Id == projectRecordId && pr.IsActive);
        }

        // 2. Applicant: if we have project record, check if the user is the creator (UserId) of the project record
        if (projectRecord != null && projectRecord.UserId == userId)
        {
            return true;
        }

        // No access path found
        return false;
    }

    public async Task<bool> HasDocumentAccess(string userId, Guid documentId)
    {
        var document = await irasContext.ModificationDocuments
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == documentId && d.UserId == userId);

        if (document != null)
        {
            return true;
        }

        // No access path found
        return false;
    }

    private async Task<bool> SponsorHasAccessToProjectRecord(Guid userGuid, string projectRecordId)
    {
        var primarySponsor = await irasContext.ProjectRecordAnswers
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.ProjectRecordId == projectRecordId && a.QuestionId == "IQA0312");

        var sponsorUserExists = false;

        if (primarySponsor != null && !string.IsNullOrWhiteSpace(primarySponsor.Response))
        {
            var rtsId = primarySponsor.Response;

            sponsorUserExists = await irasContext.SponsorOrganisationsUsers
                .AsNoTracking()
                .AnyAsync
                (
                    sou =>
                        sou.UserId == userGuid &&
                        sou.RtsId == rtsId &&
                        sou.IsActive
                );
        }

        return sponsorUserExists;
    }
}