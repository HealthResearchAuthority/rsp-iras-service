using Mapster;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

public class ApplicationsService(IApplicationRepository applicationRepository) : IApplicationsService
{
    public async Task<ApplicationResponse> CreateApplication(ApplicationRequest createApplicationRequest)
    {
        var irasApplication = createApplicationRequest.Adapt<ResearchApplication>();
        var respondent = createApplicationRequest.Respondent.Adapt<Respondent>();

        irasApplication.Respondent = respondent;

        var irasAppFromDb = await applicationRepository.CreateApplication(irasApplication);

        return irasAppFromDb.Adapt<ApplicationResponse>();
    }

    public async Task<ApplicationResponse> GetApplication(string applicationId)
    {
        var specification = new GetApplicationSpecification(id: applicationId);

        var irasAppFromDb = await applicationRepository.GetApplication(specification);

        return irasAppFromDb.Adapt<ApplicationResponse>();
    }

    public async Task<ApplicationResponse> GetApplication(string applicationId, string applicationStatus)
    {
        var specification = new GetApplicationSpecification(applicationStatus, applicationId);

        var irasAppFromDb = await applicationRepository.GetApplication(specification);

        return irasAppFromDb.Adapt<ApplicationResponse>();
    }

    public async Task<IEnumerable<ApplicationResponse>> GetApplications()
    {
        var specification = new GetApplicationSpecification();

        var applicationsFromDb = await applicationRepository.GetApplications(specification);

        return applicationsFromDb.Adapt<IEnumerable<ApplicationResponse>>();
    }

    public async Task<IEnumerable<ApplicationResponse>> GetApplications(string applicationStatus)
    {
        var specification = new GetApplicationSpecification(status: applicationStatus);

        var applicationsFromDb = await applicationRepository.GetApplications(specification);

        return applicationsFromDb.Adapt<IEnumerable<ApplicationResponse>>();
    }

    public async Task<ApplicationResponse> UpdateApplication(ApplicationRequest applicationRequest)
    {
        var mappedIrasAppReq = applicationRequest.Adapt<ResearchApplication>();

        var updatedIrasAppFromDb = await applicationRepository.UpdateApplication(mappedIrasAppReq);

        return updatedIrasAppFromDb.Adapt<ApplicationResponse>();
    }
}