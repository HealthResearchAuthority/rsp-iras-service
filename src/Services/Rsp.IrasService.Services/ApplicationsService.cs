using Mapster;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Application.DTOs;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

public class ApplicationsService(IApplicationRepository applicationRepository) : IApplicationsService
{
    public async Task<CreateApplicationResponse> CreateApplication(CreateApplicationRequest irasApplicationRequest)
    {
        // map from CreateApplicationRequest -> IrasApplication

        var irasApplication = irasApplicationRequest.Adapt<IrasApplication>();

        // create application
        var irasApp = await applicationRepository.CreateApplication(irasApplication);

        return irasApp.Adapt<CreateApplicationResponse>();
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