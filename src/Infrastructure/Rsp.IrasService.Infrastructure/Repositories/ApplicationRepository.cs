using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class ApplicationRepository(IrasContext irasContext) : IApplicationRepository
{
    public async Task<ProjectApplication> CreateApplication(ProjectApplication irasApplication, ProjectApplicationRespondent respondent)
    {
        var respondentEntity = await irasContext
            .ProjectApplicationRespondents
            .SingleOrDefaultAsync(r => r.ProjectApplicationRespondentId == respondent.ProjectApplicationRespondentId);

        if (respondentEntity == null)
        {
            await irasContext.ProjectApplicationRespondents.AddAsync(respondent);
        }

        irasApplication.ProjectApplicationRespondentId = respondent.ProjectApplicationRespondentId;

        var entity = await irasContext.ProjectApplications.AddAsync(irasApplication);

        await irasContext.SaveChangesAsync();

        return entity.Entity;
    }

    public async Task<ProjectApplication?> GetApplication(ISpecification<ProjectApplication> specification)
    {
        return await irasContext
            .ProjectApplications
            .WithSpecification(specification)
            .FirstOrDefaultAsync();
    }

    public Task<IEnumerable<ProjectApplication>> GetApplications(ISpecification<ProjectApplication> specification)
    {
        var result = irasContext
            .ProjectApplications
            .WithSpecification(specification)
            .AsEnumerable();

        return Task.FromResult(result);
    }

    public async Task<ProjectApplication?> UpdateApplication(ProjectApplication irasApplication)
    {
        var entity = await irasContext
            .ProjectApplications
            .FirstOrDefaultAsync(record => record.ProjectApplicationId == irasApplication.ProjectApplicationId);

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