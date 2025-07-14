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
                   ChiefInvestigator = projectAnswers
                       .Where(a => a.ProjectRecordId == pr.Id && a.QuestionId == ProjectRecordConstants.ChiefInvestigator)
                       .Select(a => a.Response)
                       .FirstOrDefault() ?? string.Empty,
                   LeadNation = projectAnswers
                       .Where(a => a.ProjectRecordId == pr.Id && a.QuestionId == ProjectRecordConstants.LeadNation)
                       .Select(a => a.Response)
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
                   ModificationType = DetermineModificationType(pm)
               };
    }

    private static IEnumerable<ProjectModificationResult> FilterModifications(IQueryable<ProjectModificationResult> modifications, ModificationSearchRequest searchQuery)
    {
        return modifications
            .AsEnumerable()
            .Where(x =>
                (string.IsNullOrEmpty(searchQuery.IrasId) || x.IrasId.Contains(searchQuery.IrasId, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(searchQuery.ChiefInvestigatorName) || x.ChiefInvestigator.Contains(searchQuery.ChiefInvestigatorName, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(searchQuery.ShortProjectTitle) || x.ShortProjectTitle.Contains(searchQuery.ShortProjectTitle, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(searchQuery.SponsorOrganisation) || x.SponsorOrganisation.Contains(searchQuery.SponsorOrganisation, StringComparison.OrdinalIgnoreCase)) &&
                (!searchQuery.FromDate.HasValue || x.CreatedAt >= searchQuery.FromDate.Value) &&
                (!searchQuery.ToDate.HasValue || x.CreatedAt <= searchQuery.ToDate.Value) &&
                (searchQuery.Country.Count == 0 || searchQuery.Country.Contains(x.LeadNation, StringComparer.OrdinalIgnoreCase)) &&
                (searchQuery.ModificationTypes.Count == 0 || searchQuery.ModificationTypes.Contains(x.ModificationType, StringComparer.OrdinalIgnoreCase))
            );
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
            _ => null
        };

        if (keySelector == null)
            return modifications;

        return sortDirection == "desc"
            ? modifications.OrderByDescending(keySelector)
            : modifications.OrderBy(keySelector);
    }

    private static string DetermineModificationType(ProjectModification pm)
    {
        if (pm.ProjectModificationChanges.Any
        (
            c => c.AreaOfChange == ProjectRecordConstants.ParticipatingOrgs &&
            c.SpecificAreaOfChange == ProjectRecordConstants.MajorModificationAreaOfChange
        ))
        {
            return "Modification of an important detail";
        }

        if (pm.ProjectModificationChanges.Any
        (
            c => c.AreaOfChange == ProjectRecordConstants.ParticipatingOrgs &&
            c.SpecificAreaOfChange == ProjectRecordConstants.MinorModificationAreaOfChange
        ))
        {
            return "Minor modifications";
        }

        return "Other";
    }
}