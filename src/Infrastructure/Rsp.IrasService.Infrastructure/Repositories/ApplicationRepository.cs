using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class ApplicationRepository(IrasContext irasContext) : IApplicationRepository
{
    public async Task<ResearchApplication> CreateApplication(ResearchApplication irasApplication, Respondent respondent)
    {
        var respondentEntity = await irasContext
            .Respondents
            .SingleOrDefaultAsync(r => r.RespondentId == respondent.RespondentId);

        if (respondentEntity == null)
        {
            await irasContext.Respondents.AddAsync(respondent);
        }

        irasApplication.RespondentId = respondent.RespondentId;

        var entity = await irasContext.ResearchApplications.AddAsync(irasApplication);

        await irasContext.SaveChangesAsync();

        return entity.Entity;
    }

    public async Task<ResearchApplication?> GetApplication(ISpecification<ResearchApplication> specification)
    {
        return await irasContext
            .ResearchApplications
            .WithSpecification(specification)
            .FirstOrDefaultAsync();
    }

    public Task<IEnumerable<ResearchApplication>> GetApplications(ISpecification<ResearchApplication> specification)
    {
        var result = irasContext
            .ResearchApplications
            .WithSpecification(specification)
            .AsEnumerable();

        return Task.FromResult(result);
    }

    public async Task<ResearchApplication?> UpdateApplication(ResearchApplication irasApplication)
    {
        var entity = await irasContext
            .ResearchApplications
            .FirstOrDefaultAsync(record => record.ApplicationId == irasApplication.ApplicationId);

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