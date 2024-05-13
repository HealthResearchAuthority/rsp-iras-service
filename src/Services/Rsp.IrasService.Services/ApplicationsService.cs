using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

public class ApplicationsService : IApplicationsService
{
    public Task CreateApplication(IrasApplication irasApplication)
    {
        // create application
        return Task.CompletedTask;
    }

    public Task<IrasApplication> GetApplication(int applicationId)
    {
        // get application
        return Task.FromResult(new IrasApplication { });
    }

    public Task<IEnumerable<IrasApplication>> GetApplications()
    {
        return Task.FromResult(new IrasApplication[] { }.AsEnumerable());
    }

    public Task UpdateApplication(int applicationId, IrasApplication irasApplication)
    {
        //update application
        return Task.CompletedTask;
    }
}