using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Constants;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class ProjectRecordRepository(IrasContext irasContext) : IProjectRecordRepository
{
    public async Task<ProjectRecord> CreateProjectRecord(ProjectRecord irasApplication, ProjectPersonnel respondent)
    {
        var respondentEntity = await irasContext
            .ProjectPersonnels
            .SingleOrDefaultAsync(r => r.Id == respondent.Id);

        if (respondentEntity == null)
        {
            await irasContext.ProjectPersonnels.AddAsync(respondent);
        }

        irasApplication.ProjectPersonnelId = respondent.Id;

        var entity = await irasContext.ProjectRecords.AddAsync(irasApplication);

        await irasContext.SaveChangesAsync();

        return entity.Entity;
    }

    public async Task<ProjectRecord?> GetProjectRecord(ISpecification<ProjectRecord> specification)
    {
        return await irasContext
            .ProjectRecords
            .WithSpecification(specification)
            .FirstOrDefaultAsync();
    }

    public Task<IEnumerable<ProjectRecord>> GetProjectRecords(ISpecification<ProjectRecord> specification)
    {
        var result = irasContext
            .ProjectRecords
            .WithSpecification(specification)
            .AsEnumerable();

        return Task.FromResult(result);
    }

    public async Task<(IEnumerable<ProjectRecord>, int)> GetPaginatedProjectRecords
    (
        ISpecification<ProjectRecord> projectsSpecification,
        ISpecification<ProjectRecordAnswer> projectTitlesSpecification,
        int pageIndex,
        int? pageSize,
        string? sortField,
        string? sortDirection
    )
    {
        // Apply filtering to ProjectRecords
        var filteredProjectRecords = irasContext
            .ProjectRecords
            .WithSpecification(projectsSpecification);

        // Apply filtering to ProjectRecordAnswers
        var filteredTitles = irasContext
            .ProjectRecordAnswers
            .WithSpecification(projectTitlesSpecification);

        // Join ProjectRecords with ProjectRecordAnswers (left join)
        var query = from projectRecord in filteredProjectRecords
                    join projectRecordAnswer in filteredTitles
                        on projectRecord.Id equals projectRecordAnswer.ProjectRecordId into titleGroup
                    from projectRecordAnswer in titleGroup.DefaultIfEmpty()
                    select new ProjectRecord
                    {
                        Id = projectRecord.Id,
                        ProjectPersonnelId = projectRecord.ProjectPersonnelId,
                        Description = projectRecord.Description,
                        IsActive = projectRecord.IsActive,
                        Status = projectRecord.Status,
                        CreatedDate = projectRecord.CreatedDate,
                        UpdatedDate = projectRecord.UpdatedDate,
                        CreatedBy = projectRecord.CreatedBy,
                        UpdatedBy = projectRecord.UpdatedBy,
                        IrasId = projectRecord.IrasId,
                        ProjectModifications = projectRecord.ProjectModifications,
                        Title = projectRecordAnswer != null && projectRecordAnswer.Response != null ? projectRecordAnswer.Response : projectRecord.Title
                    };

        // Count before pagination
        var count = await query.CountAsync();

        // Apply sorting
        query = (sortField?.ToLower(), sortDirection?.ToLower()) switch
        {
            ("title", "asc") => query.OrderBy(x => x.Title),
            ("title", "desc") => query.OrderByDescending(x => x.Title),
            ("status", "asc") => query.OrderBy(x => x.Status),
            ("status", "desc") => query.OrderByDescending(x => x.Status),
            ("createddate", "asc") => query.OrderBy(x => x.CreatedDate),
            ("createddate", "desc") => query.OrderByDescending(x => x.CreatedDate),
            ("irasid", "asc") => query.OrderBy(x => x.IrasId),
            ("irasid", "desc") => query.OrderByDescending(x => x.IrasId),
            _ => query.OrderByDescending(x => x.CreatedDate),
        };

        // Apply pagination
        if (pageSize.HasValue)
        {
            query = query
                .Skip((pageIndex - 1) * pageSize.Value)
                .Take(pageSize.Value);
        }

        var result = await query.ToListAsync();

        return (result, count);
    }

    public async Task<ProjectRecord?> UpdateProjectRecord(ProjectRecord irasApplication)
    {
        var entity = await irasContext
            .ProjectRecords
            .FirstOrDefaultAsync(record => record.Id == irasApplication.Id);

        if (entity == null)
        {
            return null;
        }

        entity.Title = irasApplication.Title;
        entity.Description = irasApplication.Description;
        entity.UpdatedDate = irasApplication.UpdatedDate;
        entity.CreatedDate = irasApplication.CreatedDate;
        entity.UpdatedBy = irasApplication.UpdatedBy;
        entity.Status = irasApplication.Status;
        entity.IrasId = irasApplication.IrasId;

        await irasContext.SaveChangesAsync();

        return entity;
    }

    public IEnumerable<ProjectModificationResult> GetModifications
    (
        ModificationSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection
    )
    {
        var modifications = ProjectModificationQuery();

        var filtered = FilterModifications(modifications, searchQuery);
        var sorted = SortModifications(filtered, sortField, sortDirection);

        return sorted
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public int GetModificationsCount(ModificationSearchRequest searchQuery)
    {
        var modifications = ProjectModificationQuery();
        return FilterModifications(modifications, searchQuery).Count();
    }

    private IQueryable<ProjectModificationResult> ProjectModificationQuery()
    {
        var projectRecords = irasContext.ProjectRecords.AsQueryable();
        var projectAnswers = irasContext.ProjectRecordAnswers.AsQueryable();

        return from pm in irasContext.ProjectModifications.Include(pm => pm.ProjectModificationChanges)
               join pr in projectRecords on pm.ProjectRecordId equals pr.Id
               select new ProjectModificationResult
               {
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
                   CreatedAt = pm.CreatedDate
               };
    }

    private static IEnumerable<ProjectModificationResult> FilterModifications(IQueryable<ProjectModificationResult> modifications, ModificationSearchRequest searchQuery)
    {
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
                    var parts = mod.ParticipatingNation.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    var mapped = parts
                        .Select(code => ProjectRecordConstants.NationIdMap.TryGetValue(code, out var name) ? name : null)
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
                && (!searchQuery.FromDate.HasValue
                    || x.CreatedAt >= searchQuery.FromDate.Value)
                && (!searchQuery.ToDate.HasValue
                    || x.CreatedAt <= searchQuery.ToDate.Value)
                && (searchQuery.LeadNation.Count == 0
                    || searchQuery.LeadNation.Contains(x.LeadNation, StringComparer.OrdinalIgnoreCase))
                && (searchQuery.ParticipatingNation.Count == 0
                    || x.ParticipatingNation
                        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .Any(pn => searchQuery.ParticipatingNation.Contains(pn, StringComparer.OrdinalIgnoreCase)))
                && (searchQuery.ModificationTypes.Count == 0
                    || searchQuery.ModificationTypes.Contains(x.ModificationType, StringComparer.OrdinalIgnoreCase)));
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
        var random = new Random();
        var modificationType = random.Next(1, 3);
        return modificationType switch
        {
            1 => "Modification of an important detail",
            2 => "Minor modification",
            _ => ""
        };
    }
}