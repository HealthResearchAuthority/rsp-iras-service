﻿using Mapster;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Application.Repositories;
using Rsp.IrasService.Application.Requests;
using Rsp.IrasService.Application.Responses;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

public class ApplicationsService(IApplicationRepository applicationRepository) : IApplicationsService
{
    public async Task<CreateApplicationResponse> CreateApplication(CreateApplicationRequest createApplicationRequest)
    {
        var irasApplication = createApplicationRequest.Adapt<IrasApplication>();

        var irasAppFromDb = await applicationRepository.CreateApplication(irasApplication);

        return irasAppFromDb.Adapt<CreateApplicationResponse>();
    }

    public async Task<GetApplicationResponse> GetApplication(int applicationId)
    {
        var specification = new GetApplicationSpecification(applicationId);

        var irasAppFromDb = await applicationRepository.GetApplication(specification);

        return irasAppFromDb.Adapt<GetApplicationResponse>();
    }

    public async Task<GetApplicationResponse> GetApplication(int applicationId, string applicationStatus)
    {
        var specification = new GetApplicationSpecification(applicationStatus, applicationId);

        var irasAppFromDb = await applicationRepository.GetApplication(specification);

        return irasAppFromDb.Adapt<GetApplicationResponse>();
    }

    public async Task<IEnumerable<GetApplicationResponse>> GetApplications()
    {
        var specification = new GetApplicationSpecification();

        var applicationsFromDb = await applicationRepository.GetApplications(specification);

        return applicationsFromDb.Adapt<IEnumerable<GetApplicationResponse>>();
    }

    public async Task<IEnumerable<GetApplicationResponse>> GetApplications(string applicationStatus)
    {
        var specification = new GetApplicationSpecification(applicationStatus);

        var applicationsFromDb = await applicationRepository.GetApplications(specification);

        return applicationsFromDb.Adapt<IEnumerable<GetApplicationResponse>>();
    }

    public async Task<CreateApplicationResponse> UpdateApplication(int applicationId, CreateApplicationRequest createApplicationRequest)
    {
        var mappedIrasAppReq = createApplicationRequest.Adapt<IrasApplication>();

        var updatedIrasAppFromDb = await applicationRepository.UpdateApplication(applicationId, mappedIrasAppReq);

        return updatedIrasAppFromDb.Adapt<CreateApplicationResponse>();
    }
}