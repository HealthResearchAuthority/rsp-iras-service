using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class ApplicationRepository(IrasContext irasContext) : IApplicationRepository
{
    public async Task<IrasApplication> CreateApplication(IrasApplication irasApplication)
    {
        var entity = await irasContext.IrasApplications.AddAsync(irasApplication);

        await irasContext.SaveChangesAsync();

        return entity.Entity;
    }

    public async Task<IrasApplication> GetApplication(int applicationId)
    {
        var entity = await irasContext.IrasApplications.FirstOrDefaultAsync(record => record.Id == applicationId);

        if (entity != null) return entity;

        // Error handling
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IrasApplication>> GetApplications()
    {
        return Task.FromResult(irasContext.IrasApplications.AsEnumerable());
    }

    public async Task<IrasApplication> UpdateApplication(int applicationId, IrasApplication irasApplication)
    {
        var entity = irasContext.IrasApplications.FirstOrDefault(record => record.Id == applicationId);

        if (entity != null)
        {
            entity.Title = irasApplication.Title;
            entity.Location = irasApplication.Location;
            entity.StartDate = irasApplication.StartDate;
            entity.ApplicationCategories = irasApplication.ApplicationCategories;
            entity.ProjectCategory = irasApplication.ProjectCategory;

            await irasContext.SaveChangesAsync();

            return entity;
        }

        // Error handling
        throw new NotImplementedException();
    }
}