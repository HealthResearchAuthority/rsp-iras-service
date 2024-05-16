using Mapster;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Application.Repositories;
using Rsp.IrasService.Application.Requests;
using Rsp.IrasService.Application.Responses;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

public class ApplicationsService(IApplicationRepository applicationRepository) : IApplicationsService
{
    public async Task<CreateApplicationResponse> CreateApplication(CreateApplicationRequest irasApplicationRequest)
    {
        var mappedIrasAppReq = irasApplicationRequest.Adapt<IrasApplication>();

        var irasAppFromDb = await applicationRepository.CreateApplication(mappedIrasAppReq);

        return irasAppFromDb.Adapt<CreateApplicationResponse>();
    }

    public async Task<GetApplicationResponse> GetApplication(int applicationId)
    {
        var irasAppFromDb = await applicationRepository.GetApplication(applicationId);

        return irasAppFromDb.Adapt<GetApplicationResponse>();
    }

    public async Task<IEnumerable<GetApplicationResponse>> GetApplications()
    {
        var applicationsFromDb = await applicationRepository.GetApplications();

        return applicationsFromDb.Adapt<IEnumerable<GetApplicationResponse>>();
    }

    public async Task<CreateApplicationResponse> UpdateApplication(int applicationId, CreateApplicationRequest irasApplicationRequest)
    {
        var mappedIrasAppReq = irasApplicationRequest.Adapt<IrasApplication>();

        var updatedIrasAppFromDb = await applicationRepository.UpdateApplication(applicationId, mappedIrasAppReq);

        return updatedIrasAppFromDb.Adapt<CreateApplicationResponse>();
    }
}