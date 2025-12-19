using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class ProjectClosureRepository(IrasContext irasContext) : IProjectClosureRepository
{
    public async Task<ProjectClosure> CreateProjectClosure(ProjectClosure projectClosure)
    {
        var entity = await irasContext.ProjectClosures.AddAsync(projectClosure);
        await irasContext.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<ProjectClosure?> GetProjectClosure(ISpecification<ProjectClosure> specification)
    {
        return await irasContext
           .ProjectClosures
           .WithSpecification(specification)
           .FirstOrDefaultAsync();
    }

    public async Task<ProjectClosure> UpdateProjectClosureStatus(ProjectClosure projectClosure)
    {
        var entity = await irasContext
            .ProjectClosures
            .FirstOrDefaultAsync(record => record.ProjectRecordId == projectClosure.ProjectRecordId);

        if (entity == null)
        {
            return null;
        }

        entity.Status = projectClosure.Status;
        entity.UpdatedBy = projectClosure.UpdatedBy;
        return entity;
    }
}