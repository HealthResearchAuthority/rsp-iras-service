using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
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
}