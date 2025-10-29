using System.Security.Cryptography;
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
                          Status = pm.Status
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
                   SentToSponsorDate = pm.SentToSponsorDate
               };
    }

    private static IEnumerable<ProjectModificationResult> FilterModifications(
        IQueryable<ProjectModificationResult> modifications, ModificationSearchRequest searchQuery)
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
                && (string.IsNullOrEmpty(searchQuery.ModificationId)
                 || x.ModificationId.Contains(searchQuery.ModificationId, StringComparison.OrdinalIgnoreCase))
                && (string.IsNullOrEmpty(searchQuery.ChiefInvestigatorName)
                    || x.ChiefInvestigator.Contains(searchQuery.ChiefInvestigatorName,
                        StringComparison.OrdinalIgnoreCase))
                && (string.IsNullOrEmpty(searchQuery.ShortProjectTitle)
                    || x.ShortProjectTitle.Contains(searchQuery.ShortProjectTitle, StringComparison.OrdinalIgnoreCase))
                && (string.IsNullOrEmpty(searchQuery.SponsorOrganisation)
                    || x.SponsorOrganisation.Contains(searchQuery.SponsorOrganisation,
                        StringComparison.OrdinalIgnoreCase))
                // ✅ Date-only filtering (ignore time)
                && (!fromDate.HasValue || x.SentToRegulatorDate!.Value.Date >= fromDate.Value)
                && (!toDate.HasValue || x.SentToRegulatorDate!.Value.Date <= toDate.Value)
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
                && (string.IsNullOrEmpty(searchQuery.ModificationType) ||
                    x.ModificationType.Contains(searchQuery.ModificationType, StringComparison.OrdinalIgnoreCase))
                && (string.IsNullOrEmpty(searchQuery.Status) ||
                    x.Status.Contains(searchQuery.Status, StringComparison.OrdinalIgnoreCase)));
    }

    private static IEnumerable<ProjectModificationResult> SortModifications(
        IEnumerable<ProjectModificationResult> modifications, string sortField, string sortDirection)
    {
        Func<ProjectModificationResult, object>? keySelector = sortField switch
        {
            nameof(ProjectModificationResult.ModificationNumber) => x => x.ModificationNumber,
            nameof(ProjectModificationResult.ChiefInvestigator) => x => x.ChiefInvestigator.ToLowerInvariant(),
            nameof(ProjectModificationResult.ShortProjectTitle) => x => x.ShortProjectTitle.ToLowerInvariant(),
            nameof(ProjectModificationResult.ModificationType) => x => x.ModificationType.ToLowerInvariant(),
            nameof(ProjectModificationResult.SponsorOrganisation) => x => x.SponsorOrganisation.ToLowerInvariant(),
            nameof(ProjectModificationResult.LeadNation) => x => x.LeadNation.ToLowerInvariant(),
            nameof(ProjectModificationResult.CreatedAt) => x => x.CreatedAt,
            nameof(ProjectModificationResult.Status) => x => x.Status,
            nameof(ProjectModificationResult.SentToRegulatorDate) => x => x.SentToRegulatorDate!,
            _ => null
        };

        if (keySelector == null)
            return modifications;

        if (sortField == nameof(ProjectModificationResult.ModificationNumber))
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
    private IQueryable<ProjectOverviewDocumentResult> ProjectOverviewDocumentsQuery(
        ProjectOverviewDocumentSearchRequest searchQuery,
        string? projectRecordId = null)
    {
        var projectRecords = irasContext.ProjectRecords.AsQueryable();
        var modificationDocuments = irasContext.ModificationDocuments.AsQueryable();
        var modificationDocumentAnswers = irasContext.ModificationDocumentAnswers.AsQueryable();

        var query = from pm in irasContext.ProjectModifications
                    join pr in projectRecords on pm.ProjectRecordId equals pr.Id
                    join pmc in irasContext.ProjectModificationChanges on pm.Id equals pmc.ProjectModificationId
                    join md in modificationDocuments on pmc.Id equals md.ProjectModificationChangeId
                    where string.IsNullOrEmpty(projectRecordId) || pr.Id == projectRecordId
                    select new
                    {
                        md.Id,
                        md.FileName,
                        md.DocumentStoragePath,
                        DocumentType = modificationDocumentAnswers
                            .Where(a => a.ModificationDocumentId == md.Id && a.QuestionId == ModificationQuestionIds.DocumentType)
                            .Select(a => a.SelectedOptions)
                            .FirstOrDefault(),
                        DocumentVersion = modificationDocumentAnswers
                            .Where(a => a.ModificationDocumentId == md.Id && a.QuestionId == ModificationQuestionIds.DocumentVersion)
                            .Select(a => a.Response)
                            .FirstOrDefault(),
                        DocumentDateRaw = modificationDocumentAnswers
                            .Where(a => a.ModificationDocumentId == md.Id && a.QuestionId == ModificationQuestionIds.DocumentDate)
                            .Select(a => a.Response)
                            .FirstOrDefault(),
                        Status = md.Status ?? string.Empty,
                        pm.ModificationIdentifier,
                        pm.ModificationNumber
                    };

        return query
            .AsEnumerable() // switch to in-memory for parsing
            .Select(x =>
            {
                DateTime? parsedDate = null;

                if (!string.IsNullOrWhiteSpace(x.DocumentDateRaw) && DateTime.TryParseExact(
                        x.DocumentDateRaw,
                        "yyyy-MM-dd",
                        null,
                        System.Globalization.DateTimeStyles.None,
                        out var dt))
                {
                    parsedDate = dt;
                }

                return new ProjectOverviewDocumentResult
                {
                    Id = x.Id,
                    FileName = x.FileName,
                    DocumentStoragePath = x.DocumentStoragePath,

                    DocumentType = !string.IsNullOrEmpty(x.DocumentType) &&
                                   searchQuery.DocumentTypes.TryGetValue(x.DocumentType, out var friendlyName)
                        ? friendlyName
                        : x.DocumentType ?? string.Empty,

                    DocumentVersion = x.DocumentVersion ?? string.Empty,
                    DocumentDate = parsedDate, // will be null if not found or invalid
                    Status = x.Status ?? string.Empty,
                    ModificationIdentifier = x.ModificationIdentifier,
                    ModificationNumber = x.ModificationNumber
                };
            })
            .AsQueryable();
    }

    private static IEnumerable<ProjectOverviewDocumentResult> FilterProjectOverviewDocuments(
        IQueryable<ProjectOverviewDocumentResult> modifications, ProjectOverviewDocumentSearchRequest searchQuery)
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
            var documents = irasContext.ModificationDocuments
                .Where(md => md.ProjectModificationChangeId == change.Id)
                .ToList();

            foreach (var doc in documents)
            {
                doc.Status = status;
            }

            change.Status = status;
            change.UpdatedDate = DateTime.Now;
        }

        modification.Status = status;
        modification.UpdatedDate = DateTime.Now;
        modification.SentToRegulatorDate = status is ModificationStatus.WithRegulator or ModificationStatus.Approved
                                        ? DateTime.Now : null;
        modification.SentToSponsorDate = status is ModificationStatus.WithSponsor ? DateTime.Now : null;

        // Save the changes to the database.
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
            .Where(d => changeIds.Contains(d.ProjectModificationChangeId))
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
}