using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Constants;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class AccessValidationRepository(IrasContext irasContext) : IAccessValidationRepository
{
    /// <summary>
    /// Returns true if any of the access paths from the SQL exist for the given user and optional ids.
    /// The method checks in order: applicant (project record owner), modification sponsor, modification reviewer, sponsor organisation project access.
    /// Short-circuits when any path grants access.
    /// </summary>
    public async Task<bool> HasAccessAsync(string userId, string? projectRecordId = null, Guid? modificationId = null)
    {
        var projectRecord = default(ProjectRecord);
        var modification = default(ProjectModification);

        if (modificationId != null && modificationId != Guid.Empty)
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

        // 1. Applicant: if we have project record, check if the user is the creator (UserId) of the project record
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
            var primarySponsor = await irasContext.ProjectRecordAnswers
                .AsNoTracking()
                .FirstOrDefaultAsync(pra => pra.ProjectRecordId == modification.ProjectRecordId && pra.QuestionId == "IQA0312");

            if (primarySponsor != null && !string.IsNullOrWhiteSpace(primarySponsor.Response))
            {
                // primarySponsor.Response contains RtsId of sponsor organisation
                var rtsId = primarySponsor.Response;

                var sponsorUserExists = await irasContext.SponsorOrganisationsUsers
                    .AsNoTracking()
                    .AnyAsync(sou => sou.RtsId == rtsId && sou.IsActive && sou.UserId == userGuid);

                if (sponsorUserExists)
                {
                    return true;
                }
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
        if (projectRecord?.Status is ProjectRecordStatus.Active)
        {
            var primarySponsor = await irasContext.ProjectRecordAnswers
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ProjectRecordId == projectRecordId && a.QuestionId == "IQA0312");

            if (primarySponsor != null && !string.IsNullOrWhiteSpace(primarySponsor.Response))
            {
                var rtsId = primarySponsor.Response;

                var sponsorUserExists = await irasContext.SponsorOrganisationsUsers
                    .AsNoTracking()
                    .AnyAsync
                    (
                        sou =>
                            sou.UserId == userGuid &&
                            sou.RtsId == rtsId &&
                            sou.IsActive
                    );

                if (sponsorUserExists)
                {
                    return true;
                }
            }
        }

        // No access path found
        return false;
    }
}