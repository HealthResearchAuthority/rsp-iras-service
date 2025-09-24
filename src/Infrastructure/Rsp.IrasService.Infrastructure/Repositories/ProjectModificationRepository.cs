using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Constants;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
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
    public async Task<ProjectModificationChange> CreateModificationChange(ProjectModificationChange projectModificationChange)
    {
        var entity = await irasContext.ProjectModificationChanges.AddAsync(projectModificationChange);

        await irasContext.SaveChangesAsync();

        return entity.Entity;
    }

    public IEnumerable<ProjectModificationResult> GetModifications(
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

    public IEnumerable<ProjectModificationResult> GetModificationsByIds(List<string> Ids)
    {
        var results = from pm in irasContext.ProjectModifications.Include(pm => pm.ProjectModificationChanges)
                      join pr in irasContext.ProjectRecords on pm.ProjectRecordId equals pr.Id
                      where Ids.Contains(pm.Id.ToString())
                      select new ProjectModificationResult
                      {
                          Id = pm.Id.ToString(),
                          ModificationId = pm.ModificationIdentifier,
                          ShortProjectTitle = irasContext.ProjectRecordAnswers
                              .Where(a => a.ProjectRecordId == pr.Id && a.QuestionId == ProjectRecordConstants.ShortProjectTitle)
                              .Select(a => a.Response)
                              .FirstOrDefault() ?? string.Empty,
                      };

        return results.OrderBy(r => r.ShortProjectTitle);
    }

    public async Task AssignModificationsToReviewer(List<string> modificationIds, string reviewerId)
    {
        var modifications = await irasContext.ProjectModifications
            .Where(pm => modificationIds.Contains(pm.Id.ToString()))
            .ToListAsync();

        foreach (var modification in modifications)
        {
            modification.ReviewerId = reviewerId;
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

    public int GetDocumentsForProjectOverviewCount(ProjectOverviewDocumentSearchRequest searchQuery, string? projectRecordId = null)
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
                   ChiefInvestigator = projectAnswers
                       .Where(a => a.ProjectRecordId == pr.Id && a.QuestionId == ProjectRecordConstants.ChiefInvestigator)
                       .Select(a => a.Response)
                       .FirstOrDefault() ?? string.Empty,
                   LeadNation = projectAnswers
                       .Where(a => a.ProjectRecordId == pr.Id && a.QuestionId == ProjectRecordConstants.LeadNation)
                       .Select(a => a.SelectedOptions)
                       .FirstOrDefault() ?? string.Empty,
                   ParticipatingNation = projectAnswers
                       .Where(a => a.ProjectRecordId == pr.Id && a.QuestionId == ProjectRecordConstants.ParticipatingNation)
                       .Select(a => a.SelectedOptions)
                       .FirstOrDefault() ?? string.Empty,
                   ShortProjectTitle = projectAnswers
                       .Where(a => a.ProjectRecordId == pr.Id && a.QuestionId == ProjectRecordConstants.ShortProjectTitle)
                       .Select(a => a.Response)
                       .FirstOrDefault() ?? string.Empty,
                   SponsorOrganisation = projectAnswers
                       .Where(a => a.ProjectRecordId == pr.Id && a.QuestionId == ProjectRecordConstants.SponsorOrganisation)
                       .Select(a => a.Response)
                       .FirstOrDefault() ?? string.Empty,
                   CreatedAt = pm.CreatedDate,
                   ReviewerId = pm.ReviewerId,
                   Status = pm.Status
               };
    }

    private static IEnumerable<ProjectModificationResult> FilterModifications(IQueryable<ProjectModificationResult> modifications, ModificationSearchRequest searchQuery)
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

                mod.ModificationType = DetermineModificationType();

                return mod;
            })
            .Where(x =>
                (string.IsNullOrEmpty(searchQuery.IrasId)
                 || x.IrasId.Contains(searchQuery.IrasId, StringComparison.OrdinalIgnoreCase))
                && (string.IsNullOrEmpty(searchQuery.ChiefInvestigatorName)
                    || x.ChiefInvestigator.Contains(searchQuery.ChiefInvestigatorName, StringComparison.OrdinalIgnoreCase))
                && (string.IsNullOrEmpty(searchQuery.ShortProjectTitle)
                    || x.ShortProjectTitle.Contains(searchQuery.ShortProjectTitle, StringComparison.OrdinalIgnoreCase))
                && (string.IsNullOrEmpty(searchQuery.SponsorOrganisation)
                    || x.SponsorOrganisation.Contains(searchQuery.SponsorOrganisation, StringComparison.OrdinalIgnoreCase))
                // ✅ Date-only filtering (ignore time)
                && (!fromDate.HasValue || x.CreatedAt.Date >= fromDate.Value)
                && (!toDate.HasValue || x.CreatedAt.Date <= toDate.Value)
                && (searchQuery.LeadNation.Count == 0
                    || searchQuery.LeadNation.Contains(x.LeadNation, StringComparer.OrdinalIgnoreCase))
                && (searchQuery.ParticipatingNation.Count == 0
                    || x.ParticipatingNation
                        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .Any(pn => searchQuery.ParticipatingNation.Contains(pn, StringComparer.OrdinalIgnoreCase)))
                && (searchQuery.ModificationTypes.Count == 0
                    || searchQuery.ModificationTypes.Contains(x.ModificationType, StringComparer.OrdinalIgnoreCase))
                && (!searchQuery.IncludeReviewerId
                    || x.ReviewerId == searchQuery.ReviewerId));
    }

    private static IEnumerable<ProjectModificationResult> SortModifications(IEnumerable<ProjectModificationResult> modifications, string sortField, string sortDirection)
    {
        Func<ProjectModificationResult, object>? keySelector = sortField switch
        {
            nameof(ProjectModificationResult.ModificationId) => x => x.ModificationId.ToLowerInvariant(),
            nameof(ProjectModificationResult.ChiefInvestigator) => x => x.ChiefInvestigator.ToLowerInvariant(),
            nameof(ProjectModificationResult.ShortProjectTitle) => x => x.ShortProjectTitle.ToLowerInvariant(),
            nameof(ProjectModificationResult.ModificationType) => x => x.ModificationType.ToLowerInvariant(),
            nameof(ProjectModificationResult.SponsorOrganisation) => x => x.SponsorOrganisation.ToLowerInvariant(),
            nameof(ProjectModificationResult.LeadNation) => x => x.LeadNation.ToLowerInvariant(),
            nameof(ProjectModificationResult.CreatedAt) => x => x.CreatedAt,
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

    private static string DetermineModificationType()
    {
        var modificationType = RandomNumberGenerator.GetInt32(1, 3);
        return modificationType switch
        {
            1 => "Modification of an important detail",
            2 => "Minor modification",
            _ => ""
        };
    }

    /// <summary>
    /// Builds an IQueryable of project overview documents by walking down
    /// ProjectModifications → ProjectModificationChanges → ModificationDocuments → ModificationDocumentAnswers.
    /// </summary>
    private IQueryable<ProjectOverviewDocumentResult> ProjectOverviewDocumentsQuery(ProjectOverviewDocumentSearchRequest searchQuery, string? projectRecordId = null)
    {
        var projectRecords = irasContext.ProjectRecords.AsQueryable();
        var modificationDocuments = irasContext.ModificationDocuments.AsQueryable();
        var modificationDocumentAnswers = irasContext.ModificationDocumentAnswers.AsQueryable();

        var query = from pm in irasContext.ProjectModifications
                    join pr in projectRecords on pm.ProjectRecordId equals pr.Id
                    join pmc in irasContext.ProjectModificationChanges on pm.Id equals pmc.ProjectModificationId
                    join md in modificationDocuments on pmc.Id equals md.ProjectModificationChangeId
                    where string.IsNullOrEmpty(projectRecordId) || pr.Id == projectRecordId
                    select new ProjectOverviewDocumentResult
                    {
                        FileName = md.FileName,
                        DocumentStoragePath = md.DocumentStoragePath,
                        DocumentType = modificationDocumentAnswers
                            .Where(a => a.ModificationDocumentId == md.Id && a.QuestionId == ModificationQuestionIds.DocumentType)
                            .Select(a => a.SelectedOptions)
                            .FirstOrDefault(),
                        DocumentVersion = modificationDocumentAnswers
                            .Where(a => a.ModificationDocumentId == md.Id && a.QuestionId == ModificationQuestionIds.DocumentVersion)
                            .Select(a => a.Response)
                            .FirstOrDefault(),
                        DocumentDate = modificationDocumentAnswers
                            .Where(a => a.ModificationDocumentId == md.Id && a.QuestionId == ModificationQuestionIds.DocumentDate)
                            .Select(a => a.Response)
                            .FirstOrDefault(),
                        Status = pmc.Status ?? string.Empty,
                        ModificationIdentifier = pm.ModificationIdentifier
                    };

        return query
            .AsEnumerable()
            .Select(x => new ProjectOverviewDocumentResult
            {
                FileName = x.FileName,
                DocumentStoragePath = x.DocumentStoragePath,

                // Lookup dictionary: if key not found, fall back to code
                DocumentType = !string.IsNullOrEmpty(x.DocumentType) &&
                               searchQuery.DocumentTypes.TryGetValue(x.DocumentType, out var friendlyName)
                    ? friendlyName
                    : x.DocumentType ?? string.Empty,

                DocumentVersion = x.DocumentVersion ?? string.Empty,
                DocumentDate = DateTime.TryParse(x.DocumentDate, out var parsedDate)
                    ? parsedDate.ToString("dd MMMM yyyy")
                    : string.Empty,

                Status = x.Status ?? string.Empty,
                ModificationIdentifier = x.ModificationIdentifier
            }).AsQueryable();
    }

    private static IEnumerable<ProjectOverviewDocumentResult> FilterProjectOverviewDocuments(IQueryable<ProjectOverviewDocumentResult> modifications, ProjectOverviewDocumentSearchRequest searchQuery)
    {
        return modifications
            .AsEnumerable()
            .Select(mod => mod)
            .Where(x =>
                (string.IsNullOrEmpty(searchQuery.IrasId)
                 || x.DocumentType.Contains(searchQuery.IrasId, StringComparison.OrdinalIgnoreCase)));
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
            nameof(ProjectOverviewDocumentResult.DocumentVersion) => x => x.DocumentVersion.ToLowerInvariant(),
            nameof(ProjectOverviewDocumentResult.DocumentDate) => x => x.DocumentDate.ToLowerInvariant(),
            nameof(ProjectOverviewDocumentResult.Status) => x => x.Status.ToLowerInvariant(),
            nameof(ProjectOverviewDocumentResult.ModificationIdentifier) => x => x.ModificationIdentifier.ToLowerInvariant(),
            _ => null
        };

        if (keySelector == null)
            return modifications.OrderBy(x => x.DocumentType.ToLowerInvariant());

        return string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase)
        ? modifications.OrderByDescending(keySelector)
        : modifications.OrderBy(keySelector);
    }
}