using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure.Helpers;

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

    public IEnumerable<ProjectModificationResult> GetModifications(ModificationSearchRequest searchQuery, int pageNumber, int pageSize)
    {
        var result = JsonHelper.Parse<ProjectModificationResult>("Modifications.json");

        return FilterModifications(result, searchQuery)
            .OrderByDescending(x => x.ModificationId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public int GetModificationsCount(ModificationSearchRequest searchQuery)
    {
        var result = JsonHelper.Parse<ProjectModificationResult>("Modifications.json");

        return FilterModifications(result, searchQuery).Count();
    }

    private static IEnumerable<ProjectModificationResult> FilterModifications(List<ProjectModificationResult> modifications, ModificationSearchRequest searchQuery)
    {
        return modifications
            .Where(x =>
                (string.IsNullOrEmpty(searchQuery.IrasId) || x.IrasId.Contains(searchQuery.IrasId, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(searchQuery.ChiefInvestigatorName) || x.ChiefInvestigator.Contains(searchQuery.ChiefInvestigatorName, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(searchQuery.ShortProjectTitle) || x.ShortProjectTitle.Contains(searchQuery.ShortProjectTitle, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(searchQuery.SponsorOrganisation) || x.SponsorOrganisation.Contains(searchQuery.SponsorOrganisation, StringComparison.OrdinalIgnoreCase)) &&
                (!searchQuery.FromDate.HasValue || x.CreatedAt >= searchQuery.FromDate.Value) &&
                (!searchQuery.ToDate.HasValue || x.CreatedAt <= searchQuery.ToDate.Value) &&
                (searchQuery.Country.Count == 0 ||
                    x.LeadNation?
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Any(nation => searchQuery.Country.Contains(nation, StringComparer.OrdinalIgnoreCase)) == true) &&
                (searchQuery.ModificationTypes.Count == 0 || searchQuery.ModificationTypes.Contains(x.ModificationType))
            );
    }
}