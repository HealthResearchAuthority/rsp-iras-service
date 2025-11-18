using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Constants;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

/// <summary>
/// Repository for managing <see cref="ProjectModification"/> and <see cref="ProjectModificationChange"/> entities in the database.
/// </summary>
public class ProjectModificationRepository(IrasContext irasContext) : IProjectModificationRepository
{
    /// <summary>
    /// Adds a new <see cref="ProjectModification"/> to the database, assigning a sequential ModificationNumber and updating the ModificationIdentifier.
    /// </summary>
    /// <param name="projectModification">The project modification entity to add.</param>
    /// <returns>The created <see cref="ProjectModification"/> entity.</returns>
    public async Task<ProjectModification> CreateModification(ProjectModification projectModification)
    {
        // Retrieve the current maximum ModificationNumber for the given ProjectRecordId.
        // This ensures that each modification for a project is sequentially numbered.
        var modificationNumber = await irasContext.ProjectModifications
            .Where(pm => pm.ProjectRecordId == projectModification.ProjectRecordId)
            .MaxAsync(pm => (int?)pm.ModificationNumber) ?? 0;

        // Increment the modification number for the new modification.
        projectModification.ModificationNumber = modificationNumber + 1;

        // Update the ModificationIdentifier to include the new ModificationNumber.
        // This typically forms a unique identifier such as "IRASID/1", "IRASID/2", etc.
        projectModification.ModificationIdentifier += projectModification.ModificationNumber;

        // Add the new ProjectModification entity to the context for tracking.
        var entity = await irasContext.ProjectModifications.AddAsync(projectModification);

        // Persist the changes to the database.
        await irasContext.SaveChangesAsync();

        // Return the newly created ProjectModification entity.
        return entity.Entity;
    }

    /// <summary>
    /// Adds a new <see cref="ProjectModificationChange"/> to the database.
    /// </summary>
    /// <param name="projectModificationChange">The project modification change entity to add.</param>
    /// <returns>The created <see cref="ProjectModificationChange"/> entity.</returns>
    public async Task<ProjectModificationChange> CreateModificationChange(
        ProjectModificationChange projectModificationChange)
    {
        var entity = await irasContext.ProjectModificationChanges.AddAsync(projectModificationChange);

        await irasContext.SaveChangesAsync();

        return entity.Entity;
    }

    public async Task<ProjectModificationChange?> GetModificationChange(
        GetModificationChangeSpecification specification)
    {
        return await irasContext
            .ProjectModificationChanges
            .WithSpecification(specification)
            .SingleOrDefaultAsync();
    }

    public Task<IEnumerable<ProjectModificationChange>> GetModificationChanges(
        GetModificationChangesSpecification specification)
    {
        var result = irasContext
            .ProjectModificationChanges
            .WithSpecification(specification)
            .AsEnumerable();

        return Task.FromResult(result);
    }

    public IEnumerable<ProjectModificationResult> GetModifications
    (
        ModificationSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        string? projectRecordId = null)
    {
        var modifications = ProjectModificationQuery(projectRecordId);

        var filtered = FilterModifications(modifications, searchQuery);
        var sorted = SortModifications(filtered, sortField, sortDirection);

        return sorted
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public int GetModificationsCount(ModificationSearchRequest searchQuery, string? projectRecordId = null)
    {
        var modifications = ProjectModificationQuery(projectRecordId);
        return FilterModifications(modifications, searchQuery).Count();
    }

    public IEnumerable<ProjectModificationResult> GetModificationsBySponsorOrganisationUser
    (
       SponsorAuthorisationsSearchRequest searchQuery,
       int pageNumber,
       int pageSize,
       string sortField,
       string sortDirection,
       Guid sponsorOrganisationUserId
    )
    {
        var modifications = ProjectModificationBySponsorOrganisationUserQuery(sponsorOrganisationUserId);
        var filtered = FilterModificationsBySponsorOrganisationUserQuery(modifications, searchQuery);
        var sorted = SortModifications(filtered, sortField, sortDirection);

        return sorted
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public int GetModificationsBySponsorOrganisationUserCount(SponsorAuthorisationsSearchRequest searchQuery, Guid sponsorOrganisationUserId)
    {
        var modifications = ProjectModificationBySponsorOrganisationUserQuery(sponsorOrganisationUserId);
        return FilterModificationsBySponsorOrganisationUserQuery(modifications, searchQuery).Count();
    }

    public IEnumerable<ProjectModificationResult> GetModificationsByIds(List<string> Ids)
    {
        var results = from pm in irasContext.ProjectModifications.Include(pm => pm.ProjectModificationChanges)
                      join pr in irasContext.ProjectRecords on pm.ProjectRecordId equals pr.Id
                      where Ids.Contains(pm.Id.ToString())
                      select new ProjectModificationResult
                      {
                          Id = pm.Id.ToString(),
                          ModificationId = pm.ModificationIdentifier,
                          ProjectRecordId = pm.ProjectRecordId,
                          ShortProjectTitle = irasContext.ProjectRecordAnswers
                              .Where(a => a.ProjectRecordId == pr.Id && a.QuestionId == ProjectRecordConstants.ShortProjectTitle)
                              .Select(a => a.Response)
                              .FirstOrDefault() ?? string.Empty,
                          Status = pm.Status,
                          CreatedAt = pm.CreatedDate,
                          ReviewerName = pm.ReviewerName
                      };

        return results.OrderBy(r => r.ShortProjectTitle);
    }

    public async Task AssignModificationsToReviewer(List<string> modificationIds, string reviewerId, string reviewerEmail, string reviewerName)
    {
        var modifications = await irasContext.ProjectModifications
            .Where(pm => modificationIds.Contains(pm.Id.ToString()))
            .ToListAsync();

        foreach (var modification in modifications)
        {
            modification.ReviewerId = reviewerId;
            modification.ReviewerEmail = reviewerEmail;
            modification.ReviewerName = reviewerName;
        }

        await irasContext.SaveChangesAsync();
    }

    public IEnumerable<ProjectOverviewDocumentResult> GetDocumentsForProjectOverview(
        ProjectOverviewDocumentSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        string? projectRecordId = null)
    {
        var modifications = ProjectOverviewDocumentsQuery(searchQuery, projectRecordId);

        var filtered = FilterProjectOverviewDocuments(modifications, searchQuery);
        var sorted = SortProjectOverviewDocuments(filtered, sortField, sortDirection);

        return sorted
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public int GetDocumentsForProjectOverviewCount(ProjectOverviewDocumentSearchRequest searchQuery,
        string? projectRecordId = null)
    {
        var modifications = ProjectOverviewDocumentsQuery(searchQuery, projectRecordId);
        return FilterProjectOverviewDocuments(modifications, searchQuery).Count();
    }

    private IQueryable<ProjectModificationResult> ProjectModificationQuery(string? projectRecordId = null)
    {
        var projectRecords = irasContext.ProjectRecords.AsQueryable();
        var projectAnswers = irasContext.ProjectRecordAnswers.AsQueryable();

        return from pm in irasContext.ProjectModifications.Include(pm => pm.ProjectModificationChanges)
               join pr in projectRecords on pm.ProjectRecordId equals pr.Id
               where string.IsNullOrEmpty(projectRecordId) || pr.Id == projectRecordId
               select new ProjectModificationResult
               {
                   Id = pm.Id.ToString(),
                   ProjectRecordId = pr.Id,
                   ModificationId = pm.ModificationIdentifier,
                   IrasId = pr.IrasId.HasValue ? pr.IrasId.Value.ToString() : string.Empty,
                   ModificationNumber = pm.ModificationNumber,
                   ModificationType = pm.ModificationType,
                   Category = pm.Category,
                   ReviewType = pm.ReviewType,
                   ChiefInvestigator = projectAnswers
                       .Where(a => a.ProjectRecordId == pr.Id && a.QuestionId == ProjectRecordConstants.ChiefInvestigator)
                       .Select(a => a.Response)
                       .FirstOrDefault() ?? string.Empty,
                   LeadNation = projectAnswers
                       .Where(a => a.ProjectRecordId == pr.Id && a.QuestionId == ProjectRecordConstants.LeadNation)
                       .Select(a => a.SelectedOptions)
                       .FirstOrDefault() ?? string.Empty,
                   ParticipatingNation = projectAnswers
                       .Where(a => a.ProjectRecordId == pr.Id &&
                                   a.QuestionId == ProjectRecordConstants.ParticipatingNation)
                       .Select(a => a.SelectedOptions)
                       .FirstOrDefault() ?? string.Empty,
                   ShortProjectTitle = projectAnswers
                       .Where(a => a.ProjectRecordId == pr.Id && a.QuestionId == ProjectRecordConstants.ShortProjectTitle)
                       .Select(a => a.Response)
                       .FirstOrDefault() ?? string.Empty,
                   SponsorOrganisation = projectAnswers
                       .Where(a => a.ProjectRecordId == pr.Id &&
                                   a.QuestionId == ProjectRecordConstants.SponsorOrganisation)
                       .Select(a => a.Response)
                       .FirstOrDefault() ?? string.Empty,
                   CreatedAt = pm.CreatedDate,
                   ReviewerId = pm.ReviewerId,
                   Status = pm.Status,
                   SentToRegulatorDate = pm.SentToRegulatorDate,
                   SentToSponsorDate = pm.SentToSponsorDate,
                   ReviewerName = pm.ReviewerName
               };
    }

    private static IEnumerable<ProjectModificationResult> FilterModifications
    (
        IQueryable<ProjectModificationResult> modifications,
        ModificationSearchRequest searchQuery
    )
    {
        var fromDate = searchQuery.FromDate?.Date;
        var toDate = searchQuery.ToDate?.Date;

        return modifications
            .AsEnumerable()
            .Select(mod =>
            {
                mod.LeadNation = ProjectRecordConstants.NationIdMap.TryGetValue(mod.LeadNation, out var leadnation)
                    ? leadnation
                    : string.Empty;

                // Map ParticipatingNation codes to names
                if (!string.IsNullOrWhiteSpace(mod.ParticipatingNation))
                {
                    var parts = mod.ParticipatingNation.Split(',',
                        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    var mapped = parts
                        .Select(code =>
                            ProjectRecordConstants.NationIdMap.TryGetValue(code, out var name) ? name : null)
                        .Where(n => !string.IsNullOrEmpty(n));
                    mod.ParticipatingNation = string.Join(", ", mapped);
                }
                else
                {
                    mod.ParticipatingNation = string.Empty;
                }

                return mod;
            })
            .Where(x =>
                (string.IsNullOrEmpty(searchQuery.IrasId)
                 || x.IrasId.Contains(searchQuery.IrasId, StringComparison.OrdinalIgnoreCase))
                && (string.IsNullOrEmpty(searchQuery.ModificationId)
                    || x.ModificationId.Contains(searchQuery.ModificationId, StringComparison.OrdinalIgnoreCase))
                && (string.IsNullOrEmpty(searchQuery.ChiefInvestigatorName)
                    || x.ChiefInvestigator.Contains(searchQuery.ChiefInvestigatorName,
                        StringComparison.OrdinalIgnoreCase))
                && (string.IsNullOrEmpty(searchQuery.ShortProjectTitle)
                    || x.ShortProjectTitle.Contains(searchQuery.ShortProjectTitle, StringComparison.OrdinalIgnoreCase))
                && (string.IsNullOrEmpty(searchQuery.SponsorOrganisation)
                    || x.SponsorOrganisation.Equals(searchQuery.SponsorOrganisation,
                        StringComparison.OrdinalIgnoreCase))
                // ✅ DateSubmitted-only filtering (ignore time)
                && (!fromDate.HasValue ||
                    (x.DateSubmitted.HasValue && x.DateSubmitted.Value.Date >= fromDate.Value))
                && (!toDate.HasValue ||
                    (x.DateSubmitted.HasValue && x.DateSubmitted.Value.Date <= toDate.Value))
                && (searchQuery.LeadNation.Count == 0
                    || searchQuery.LeadNation.Contains(x.LeadNation, StringComparer.OrdinalIgnoreCase))
                && (searchQuery.ParticipatingNation.Count == 0
                    || x.ParticipatingNation
                        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .Any(pn => searchQuery.ParticipatingNation.Contains(pn, StringComparer.OrdinalIgnoreCase)))
                && (searchQuery.ModificationTypes.Count == 0
                    || searchQuery.ModificationTypes.Contains(x.ModificationType, StringComparer.OrdinalIgnoreCase))
                && (!searchQuery.IncludeReviewerId
                    || x.ReviewerId == searchQuery.ReviewerId)
                && (!searchQuery.IncludeReviewerName
                    || (!string.IsNullOrWhiteSpace(searchQuery.ReviewerName)
                        && (x.ReviewerName ?? string.Empty)
                        .Contains(searchQuery.ReviewerName, StringComparison.OrdinalIgnoreCase)))
                && (string.IsNullOrEmpty(searchQuery.ModificationType) ||
                    x.ModificationType.Contains(searchQuery.ModificationType, StringComparison.OrdinalIgnoreCase))
                && (string.IsNullOrEmpty(searchQuery.Status) ||
                    x.Status.Contains(searchQuery.Status, StringComparison.OrdinalIgnoreCase))
                && (!searchQuery.AllowedStatuses.Any()
                    || searchQuery.AllowedStatuses.Contains(x.Status, StringComparer.OrdinalIgnoreCase)));
    }

    private static IEnumerable<ProjectModificationResult> SortModifications(
        IEnumerable<ProjectModificationResult> modifications, string sortField, string sortDirection)
    {
        Func<ProjectModificationResult, object>? keySelector = sortField switch
        {
            nameof(ProjectModificationResult.ModificationId) => x => x.ModificationId,
            nameof(ProjectModificationResult.ChiefInvestigator) => x => x.ChiefInvestigator.ToLowerInvariant(),
            nameof(ProjectModificationResult.ShortProjectTitle) => x => x.ShortProjectTitle.ToLowerInvariant(),
            nameof(ProjectModificationResult.ModificationType) => x => x.ModificationType.ToLowerInvariant(),
            nameof(ProjectModificationResult.SponsorOrganisation) => x => x.SponsorOrganisation.ToLowerInvariant(),
            nameof(ProjectModificationResult.LeadNation) => x => x.LeadNation.ToLowerInvariant(),
            nameof(ProjectModificationResult.CreatedAt) => x => x.CreatedAt,
            nameof(ProjectModificationResult.Status) => x => x.Status,
            nameof(ProjectModificationResult.SentToRegulatorDate) => x => x.SentToRegulatorDate!,
            nameof(ProjectModificationResult.SentToSponsorDate) => x => x.SentToSponsorDate!,
            nameof(ProjectModificationResult.ReviewerName) => x => x.ReviewerName,
            // ✅ New combined date field
            nameof(ProjectModificationResult.DateSubmitted) => x => x.DateSubmitted!,
            _ => null
        };

        if (keySelector == null)
            return modifications;

        if (sortField == nameof(ProjectModificationResult.ModificationId))
        {
            return sortDirection == "desc"
                ? modifications
                    .OrderByDescending(m => int.TryParse(m.IrasId, out var irasId) ? irasId : 0)
                    .ThenByDescending(m => m.ModificationNumber)
                : modifications
                    .OrderBy(m => int.TryParse(m.IrasId, out var irasId) ? irasId : 0)
                    .ThenBy(m => m.ModificationNumber);
        }

        return sortDirection == "desc"
            ? modifications.OrderByDescending(keySelector)
            : modifications.OrderBy(keySelector);
    }


    private IQueryable<ProjectModificationResult> ProjectModificationBySponsorOrganisationUserQuery(Guid sponsorOrganisationUserId)
    {
        var projectRecords = irasContext.ProjectRecords.AsQueryable();
        var projectAnswers = irasContext.ProjectRecordAnswers.AsQueryable();

        var rtsId = irasContext.SponsorOrganisationsUsers
            .Where(u => u.Id == sponsorOrganisationUserId)
            .Select(u => u.RtsId)
            .FirstOrDefault();

        return from prm in irasContext.ProjectModifications.Include(pm => pm.ProjectModificationChanges)
               join proj in projectRecords on prm.ProjectRecordId equals proj.Id
               where projectAnswers.Any(a => a.ProjectRecordId == proj.Id &&
                                             a.QuestionId == ProjectRecordConstants.SponsorOrganisation &&
                                             a.Response == rtsId)
               select new ProjectModificationResult
               {
                   Id = prm.Id.ToString(),
                   ProjectRecordId = proj.Id,
                   ModificationId = prm.ModificationIdentifier,
                   IrasId = proj.IrasId.HasValue ? proj.IrasId.Value.ToString() : string.Empty,
                   ModificationNumber = prm.ModificationNumber,
                   ChiefInvestigator = projectAnswers
                       .Where(a => a.ProjectRecordId == proj.Id && a.QuestionId == ProjectRecordConstants.ChiefInvestigator)
                       .Select(a => a.Response)
                       .FirstOrDefault() ?? string.Empty,
                   LeadNation = projectAnswers
                       .Where(a => a.ProjectRecordId == proj.Id && a.QuestionId == ProjectRecordConstants.LeadNation)
                       .Select(a => a.SelectedOptions)
                       .FirstOrDefault() ?? string.Empty,
                   ParticipatingNation = projectAnswers
                       .Where(a => a.ProjectRecordId == proj.Id && a.QuestionId == ProjectRecordConstants.ParticipatingNation)
                       .Select(a => a.SelectedOptions)
                       .FirstOrDefault() ?? string.Empty,
                   ShortProjectTitle = projectAnswers
                       .Where(a => a.ProjectRecordId == proj.Id && a.QuestionId == ProjectRecordConstants.ShortProjectTitle)
                       .Select(a => a.Response)
                       .FirstOrDefault() ?? string.Empty,
                   SponsorOrganisation = rtsId ?? string.Empty,
                   CreatedAt = prm.CreatedDate,
                   ReviewerId = prm.ReviewerId,
                   Status = prm.Status,
                   SentToRegulatorDate = prm.SentToRegulatorDate,
                   SentToSponsorDate = prm.SentToSponsorDate
               };
    }

    private static IEnumerable<ProjectModificationResult> FilterModificationsBySponsorOrganisationUserQuery(
    IQueryable<ProjectModificationResult> modifications,
    SponsorAuthorisationsSearchRequest searchQuery)
    {
        var term = searchQuery.SearchTerm?.Trim();

        return modifications
            .AsEnumerable()
            .Where(x =>
                (string.IsNullOrEmpty(term) ||
                 x.IrasId.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                 x.ModificationId.Contains(term, StringComparison.OrdinalIgnoreCase))
                &&
                (x.Status == ModificationStatus.WithSponsor ||
                 x.Status == ModificationStatus.Approved ||
                 x.Status == ModificationStatus.WithReviewBody ||
                 x.Status == ModificationStatus.NotApproved));
    }

    /// <summary>
    /// Builds an IQueryable of project overview documents by walking down
    /// ProjectModifications → ProjectModificationChanges → ModificationDocuments → ModificationDocumentAnswers.
    /// </summary>
    private IQueryable<ProjectOverviewDocumentResult> ProjectOverviewDocumentsQuery(
    ProjectOverviewDocumentSearchRequest searchQuery,
    string? projectRecordId = null)
    {
        var projectRecords = irasContext.ProjectRecords.AsQueryable();
        var docs = irasContext.ModificationDocuments.AsQueryable();

        var baseQuery =
            from pm in irasContext.ProjectModifications
            join pr in projectRecords on pm.ProjectRecordId equals pr.Id
            join md in docs on pm.Id equals md.ProjectModificationId
            where string.IsNullOrEmpty(projectRecordId) || pr.Id == projectRecordId
            select new ProjectOverviewDocumentResult
            {
                Id = md.Id,
                ProjectModificationId = md.ProjectModificationId,
                FileName = md.FileName,
                DocumentStoragePath = md.DocumentStoragePath,
                IsMalwareScanSuccessful = md.IsMalwareScanSuccessful,
                Status = md.Status ?? "",
                ModificationIdentifier = pm.ModificationIdentifier,
                ModificationNumber = pm.ModificationNumber
            };

        return BuildDocumentQuery(baseQuery, irasContext.ModificationDocumentAnswers, searchQuery);
    }

    private static IEnumerable<ProjectOverviewDocumentResult> FilterProjectOverviewDocuments(
    IQueryable<ProjectOverviewDocumentResult> docs,
    ProjectOverviewDocumentSearchRequest searchQuery)
    => ApplyDocumentFilter(docs, searchQuery);

    private static IEnumerable<ProjectOverviewDocumentResult> ApplyDocumentFilter(
    IQueryable<ProjectOverviewDocumentResult> documents,
    ProjectOverviewDocumentSearchRequest searchQuery)
    {
        return documents
            .AsEnumerable()
            .Where(x =>
                string.IsNullOrEmpty(searchQuery.IrasId) ||
                x.DocumentType.Contains(searchQuery.IrasId, StringComparison.OrdinalIgnoreCase));
    }

    private IQueryable<ProjectOverviewDocumentResult> BuildDocumentQuery(
    IQueryable<ProjectOverviewDocumentResult> baseQuery,
    IQueryable<ModificationDocumentAnswer> answersQuery,
    ProjectOverviewDocumentSearchRequest searchQuery)
    {
        var query =
            from x in baseQuery
            select new
            {
                x.Id,
                x.ProjectModificationId,
                x.FileName,
                x.DocumentStoragePath,
                x.IsMalwareScanSuccessful,
                DocumentName = answersQuery
                    .Where(a => a.ModificationDocumentId == x.Id &&
                                a.QuestionId == ModificationQuestionIds.DocumentName)
                    .Select(a => a.Response)
                    .FirstOrDefault(),
                DocumentType = answersQuery
                    .Where(a => a.ModificationDocumentId == x.Id &&
                                a.QuestionId == ModificationQuestionIds.DocumentType)
                    .Select(a => a.SelectedOptions)
                    .FirstOrDefault(),
                DocumentVersion = answersQuery
                    .Where(a => a.ModificationDocumentId == x.Id &&
                                a.QuestionId == ModificationQuestionIds.DocumentVersion)
                    .Select(a => a.Response)
                    .FirstOrDefault(),
                DocumentDateRaw = answersQuery
                    .Where(a => a.ModificationDocumentId == x.Id &&
                                a.QuestionId == ModificationQuestionIds.DocumentDate)
                    .Select(a => a.Response)
                    .FirstOrDefault(),
                x.Status,
                x.ModificationIdentifier,
                x.ModificationNumber
            };

        // now safe to materialize
        return query
            .AsEnumerable()
            .Select(MapToDocumentResult(searchQuery))
            .AsQueryable();
    }

    private static Func<dynamic, ProjectOverviewDocumentResult> MapToDocumentResult(
    ProjectOverviewDocumentSearchRequest searchQuery)
    {
        return x =>
        {
            // Parse DocumentDateRaw ("yyyy-MM-dd")
            DateTime? parsedDate = null;
            DateTime dt = default;
            string friendlyName = string.Empty;

            if (!string.IsNullOrWhiteSpace(x.DocumentDateRaw) &&
                DateTime.TryParseExact(
                    x.DocumentDateRaw,
                    "yyyy-MM-dd",
                    null,
                    System.Globalization.DateTimeStyles.None,
                    out dt))
            {
                parsedDate = dt;
            }

            // Resolve friendly name for document type
            var resolvedDocumentType =
                !string.IsNullOrEmpty(x.DocumentType) &&
                searchQuery.DocumentTypes.TryGetValue(x.DocumentType, out friendlyName)
                    ? friendlyName
                    : x.DocumentType ?? string.Empty;

            return new ProjectOverviewDocumentResult
            {
                Id = x.Id,
                ProjectModificationId = x.ProjectModificationId,
                FileName = x.FileName,
                DocumentName = x.DocumentName ?? string.Empty,
                DocumentStoragePath = x.DocumentStoragePath,
                DocumentType = resolvedDocumentType,
                DocumentVersion = x.DocumentVersion ?? string.Empty,
                DocumentDate = parsedDate,
                Status = x.Status ?? string.Empty,
                ModificationIdentifier = x.ModificationIdentifier,
                ModificationNumber = x.ModificationNumber,
                IsMalwareScanSuccessful = x.IsMalwareScanSuccessful
            };
        };
    }

    private static IEnumerable<ProjectOverviewDocumentResult> SortProjectOverviewDocuments(IEnumerable<ProjectOverviewDocumentResult> modifications, string sortField, string sortDirection)
    {
        if (string.IsNullOrWhiteSpace(sortField))
        {
            return modifications.OrderBy(x => x.DocumentType.ToLowerInvariant());
        }

        Func<ProjectOverviewDocumentResult, object>? keySelector = sortField switch
        {
            nameof(ProjectOverviewDocumentResult.DocumentType) => x => x.DocumentType.ToLowerInvariant(),
            nameof(ProjectOverviewDocumentResult.FileName) => x => x.FileName.ToLowerInvariant(),
            nameof(ProjectOverviewDocumentResult.DocumentName) => x => x.DocumentName.ToLowerInvariant(),
            nameof(ProjectOverviewDocumentResult.DocumentVersion) => x => x.DocumentVersion.ToLowerInvariant(),
            nameof(ProjectOverviewDocumentResult.DocumentDate) => x => x.DocumentDate ?? DateTime.MinValue,
            nameof(ProjectOverviewDocumentResult.Status) => x => x.Status.ToLowerInvariant(),
            nameof(ProjectOverviewDocumentResult.ModificationIdentifier) => x => x.ModificationNumber,
            _ => null
        };

        if (keySelector == null)
            return modifications.OrderBy(x => x.DocumentType.ToLowerInvariant());

        return string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase)
            ? modifications.OrderByDescending(keySelector)
            : modifications.OrderBy(keySelector);
    }

    /// <summary>
    /// Removes a single <see cref="ProjectModificationChange"/> that matches the provided specification.
    /// If no matching entity is found, the method completes without making any changes.
    /// </summary>
    /// <param name="specification">The specification used to locate the modification change to remove.</param>
    public async Task RemoveModificationChange(ISpecification<ProjectModificationChange> specification)
    {
        // Attempt to find a single ProjectModificationChange matching the given specification.
        // Using FirstOrDefaultAsync to avoid exceptions if no entity matches the criteria.
        var modificationChange = await irasContext
            .ProjectModificationChanges
            .WithSpecification(specification)
            .FirstOrDefaultAsync();

        // If no entity was found, there is nothing to remove.
        if (modificationChange == null)
        {
            return;
        }

        // Mark the entity for deletion and persist changes.
        irasContext.ProjectModificationChanges.Remove(modificationChange);

        // Save the changes to the database.
        await irasContext.SaveChangesAsync();
    }

    /// <summary>
    /// Updates the status <see cref="ProjectModification"/> of the modification that matches the provided specification.
    /// It also updates the status of the modification changes for this modification, to keep in sync
    /// If no matching entity is found, the method completes without making any changes.
    /// </summary>
    /// <param name="specification">The specification used to locate the modification to update.</param>
    public async Task UpdateModificationStatus(ISpecification<ProjectModification> specification, string status)
    {
        // Attempt to find a single ProjectModification matching the given specification.
        // Using FirstOrDefaultAsync to avoid exceptions if no entity matches the criteria.
        var modification = await irasContext
            .ProjectModifications
            .Include(pm => pm.ProjectModificationChanges)
            .WithSpecification(specification)
            .FirstOrDefaultAsync();

        // If no entity was found, there is nothing to update.
        if (modification == null)
        {
            return;
        }

        // update the modification changes status as well
        foreach (var change in modification.ProjectModificationChanges)
        {
            change.Status = status;
            change.UpdatedDate = DateTime.Now;
        }

        var documents = await irasContext.ModificationDocuments
            .Where(md => md.ProjectModificationId == modification.Id)
            .ToListAsync();

        foreach (var doc in documents)
        {
            doc.Status = status;
        }

        modification.Status = status;
        modification.UpdatedDate = DateTime.Now;

        switch (status)
        {
            case ModificationStatus.Approved or ModificationStatus.WithReviewBody:
                modification.SentToRegulatorDate ??= DateTime.Now;
                break;

            case ModificationStatus.WithSponsor:
                modification.SentToSponsorDate ??= DateTime.Now;
                break;
        }

        // Save the changes to the database.
        await irasContext.SaveChangesAsync();
    }

    /// <summary>
    /// Updates the <see cref="ProjectModification"/> that matches the provided specification.
    /// It also updates the status of the modification changes for this modification, to keep in sync
    /// If no matching entity is found, the method completes without making any changes.
    /// </summary>
    /// <param name="specification">The specification used to locate the modification to update.</param>
    public async Task UpdateModification(ISpecification<ProjectModification> specification, ProjectModification projectModification)
    {
        // Attempt to find a single ProjectModification matching the given specification.
        // Using FirstOrDefaultAsync to avoid exceptions if no entity matches the criteria.
        var existingModification = await irasContext
            .ProjectModifications
            .Include(pm => pm.ProjectModificationChanges)
            .WithSpecification(specification)
            .SingleOrDefaultAsync();

        // If no entity was found, there is nothing to update.
        if (existingModification == null)
        {
            return;
        }

        irasContext.Entry(existingModification).CurrentValues.SetValues(projectModification);

        var modificationUpdatedTime = DateTime.Now;

        // cascade update to modification changes
        foreach (var change in existingModification.ProjectModificationChanges.ToList())
        {
            // get the matching change from the input modification
            var updatedChange = projectModification.ProjectModificationChanges.FirstOrDefault(c => c.Id == change.Id);

            if (updatedChange != null)
            {
                irasContext.Entry(change).CurrentValues.SetValues(updatedChange);
            }

            change.UpdatedDate = modificationUpdatedTime;
        }

        existingModification.UpdatedDate = modificationUpdatedTime;

        await irasContext.SaveChangesAsync();
    }

    /// <summary>
    /// Updates the <see cref="ProjectModificationChange"/> that matches the provided specification.
    /// It also updates the status of the modification changes for this modification, to keep in sync
    /// If no matching entity is found, the method completes without making any changes.
    /// </summary>
    /// <param name="specification">The specification used to locate the modification to update.</param>
    public async Task UpdateModificationChange(ISpecification<ProjectModificationChange> specification, ProjectModificationChange modificationChange)
    {
        // Attempt to find a single ProjectModificationChange matching the given specification.
        // Using FirstOrDefaultAsync to avoid exceptions if no entity matches the criteria.
        var existingChange = await irasContext
            .ProjectModificationChanges
            .WithSpecification(specification)
            .SingleOrDefaultAsync();

        // If no entity was found, there is nothing to update.
        if (existingChange == null)
        {
            return;
        }

        existingChange.UpdatedDate = DateTime.Now;

        irasContext.Entry(existingChange).CurrentValues.SetValues(modificationChange);

        await irasContext.SaveChangesAsync();
    }

    public async Task DeleteModification(ISpecification<ProjectModification> specification)
    {
        var modification = await irasContext
            .ProjectModifications
            .Include(pm => pm.ProjectModificationChanges)
            .WithSpecification(specification)
            .FirstOrDefaultAsync();

        if (modification == null)
        {
            return;
        }

        var modId = modification.Id;

        // All change IDs for this modification
        var changeIds = await irasContext.ProjectModificationChanges
            .Where(c => c.ProjectModificationId == modId)
            .Select(c => c.Id)
            .ToListAsync();

        // All document IDs for those changes
        var documentIds = await irasContext.ModificationDocuments
            .Where(d => d.ProjectModificationId == modId)
            .Select(d => d.Id)
            .ToListAsync();

        // 1) Remove change answers
        var changeAnswers = await irasContext.ProjectModificationChangeAnswers
            .Where(a => changeIds.Contains(a.ProjectModificationChangeId))
            .ToListAsync();
        irasContext.ProjectModificationChangeAnswers.RemoveRange(changeAnswers);

        // 2) Remove document answers
        var documentAnswers = await irasContext.ModificationDocumentAnswers
            .Where(a => documentIds.Contains(a.ModificationDocumentId))
            .ToListAsync();
        irasContext.ModificationDocumentAnswers.RemoveRange(documentAnswers);

        // 3) Remove modification-level answers
        var modAnswers = await irasContext.ProjectModificationAnswers
            .Where(a => a.ProjectModificationId == modId)
            .ToListAsync();
        irasContext.ProjectModificationAnswers.RemoveRange(modAnswers);

        // 4) Remove documents
        var documents = await irasContext.ModificationDocuments
            .Where(d => documentIds.Contains(d.Id))
            .ToListAsync();
        irasContext.ModificationDocuments.RemoveRange(documents);

        // 5) Remove changes
        var changes = await irasContext.ProjectModificationChanges
            .Where(c => changeIds.Contains(c.Id))
            .ToListAsync();
        irasContext.ProjectModificationChanges.RemoveRange(changes);

        // 6) Remove the modification
        irasContext.ProjectModifications.Remove(modification);

        await irasContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProjectModificationAuditTrail>> GetModificationAuditTrail(Guid modificationId)
    {
        return await irasContext.ProjectModificationAuditTrail
            .Where(a => a.ProjectModificationId == modificationId)
            .OrderByDescending(a => a.DateTimeStamp)
            .ToListAsync();
    }

    /// <summary>
    /// Saves the modification review responses
    /// </summary>
    /// <param name="modificationReviewRequest">The request object containing the review values</param>
    public async Task SaveModificationReviewResponses(ModificationReviewRequest modificationReviewRequest)
    {
        var modification = await irasContext.ProjectModifications
            .FirstOrDefaultAsync(pm => pm.Id == modificationReviewRequest.ProjectModificationId);

        if (modification != null)
        {
            modification.ReviewerComments = modificationReviewRequest.Comment;
            modification.ReasonNotApproved = modificationReviewRequest.ReasonNotApproved;
            modification.ProvisionalReviewOutcome = modificationReviewRequest.Outcome;

            await irasContext.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Gets modification for a specific projectModificationId
    /// </summary>
    /// <param name="projectModificationId">The unique identifier of the modification</param>
    /// <returns>The project modification record</returns>
    public Task<ProjectModification> GetModificationById(Guid projectModificationId)
    {
        return irasContext.ProjectModifications
            .FirstAsync(pm => pm.Id == projectModificationId);
    }

    public IEnumerable<ProjectOverviewDocumentResult> GetDocumentsForModification(
        ProjectOverviewDocumentSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        Guid modificationId)
    {
        var modifications = ModificationDocumentsQuery(searchQuery, modificationId);

        var filtered = FilterModificationDocuments(modifications, searchQuery);
        var sorted = SortProjectOverviewDocuments(filtered, sortField, sortDirection);

        return sorted
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public int GetDocumentsForModificationCount(ProjectOverviewDocumentSearchRequest searchQuery,
        Guid modificationId)
    {
        var modifications = ModificationDocumentsQuery(searchQuery, modificationId);
        return FilterModificationDocuments(modifications, searchQuery).Count();
    }

    /// <summary>
    /// Builds an IQueryable of project overview documents by walking down
    /// ProjectModifications → ProjectModificationChanges → ModificationDocuments → ModificationDocumentAnswers.
    /// </summary>
    private IQueryable<ProjectOverviewDocumentResult> ModificationDocumentsQuery(
    ProjectOverviewDocumentSearchRequest searchQuery,
    Guid modificationId)
    {
        var docs = irasContext.ModificationDocuments.AsQueryable();

        var baseQuery =
            from pm in irasContext.ProjectModifications
            join md in docs on pm.Id equals md.ProjectModificationId
            where pm.Id == modificationId
            select new ProjectOverviewDocumentResult
            {
                Id = md.Id,
                ProjectModificationId = md.ProjectModificationId,
                FileName = md.FileName,
                DocumentStoragePath = md.DocumentStoragePath,
                IsMalwareScanSuccessful = md.IsMalwareScanSuccessful,
                Status = md.Status ?? "",
                ModificationIdentifier = pm.ModificationIdentifier,
                ModificationNumber = pm.ModificationNumber
            };

        return BuildDocumentQuery(baseQuery, irasContext.ModificationDocumentAnswers, searchQuery);
    }

    private static IEnumerable<ProjectOverviewDocumentResult> FilterModificationDocuments(
    IQueryable<ProjectOverviewDocumentResult> docs,
    ProjectOverviewDocumentSearchRequest searchQuery)
    => ApplyDocumentFilter(docs, searchQuery);

    public async Task<ProjectModification?> GetModification(GetModificationSpecification specification)
    {
        return await irasContext
            .ProjectModifications
            .WithSpecification(specification)
            .SingleOrDefaultAsync();
    }
}