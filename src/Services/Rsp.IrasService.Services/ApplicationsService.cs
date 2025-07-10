using Mapster;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

public class ApplicationsService(IProjectRecordRepository applicationRepository) : IApplicationsService
{
    public async Task<ApplicationResponse> CreateApplication(ApplicationRequest applicationRequest)
    {
        var irasApplication = applicationRequest.Adapt<ProjectRecord>();
        var respondent = applicationRequest.Respondent.Adapt<ProjectPersonnel>();

        var createdApplication = await applicationRepository.CreateProjectRecord(irasApplication, respondent);

        return createdApplication.Adapt<ApplicationResponse>();
    }

    public async Task<ApplicationResponse> GetApplication(string applicationId)
    {
        var specification = new GetApplicationSpecification(id: applicationId);

        var irasAppFromDb = await applicationRepository.GetProjectRecord(specification);

        return irasAppFromDb.Adapt<ApplicationResponse>();
    }

    public async Task<ApplicationResponse> GetApplication(string applicationId, string applicationStatus)
    {
        var specification = new GetApplicationSpecification(applicationStatus, applicationId);

        var irasAppFromDb = await applicationRepository.GetProjectRecord(specification);

        return irasAppFromDb.Adapt<ApplicationResponse>();
    }

    public async Task<IEnumerable<ApplicationResponse>> GetApplications()
    {
        var specification = new GetApplicationSpecification();

        var applicationsFromDb = await applicationRepository.GetProjectRecords(specification);

        return applicationsFromDb.Adapt<IEnumerable<ApplicationResponse>>();
    }

    public async Task<IEnumerable<ApplicationResponse>> GetApplications(string applicationStatus)
    {
        var specification = new GetApplicationSpecification(status: applicationStatus);

        var applicationsFromDb = await applicationRepository.GetProjectRecords(specification);

        return applicationsFromDb.Adapt<IEnumerable<ApplicationResponse>>();
    }

    public async Task<IEnumerable<ApplicationResponse>> GetRespondentApplications(string respondentId)
    {
        var specification = new GetRespondentApplicationSpecification(respondentId: respondentId);

        var applicationsFromDb = await applicationRepository.GetProjectRecords(specification);

        return applicationsFromDb.Adapt<IEnumerable<ApplicationResponse>>();
    }

    // This overload uses pagination
    public async Task<IEnumerable<ApplicationResponse>> GetRespondentApplications(string respondentId, string? searchQuery, int pageIndex, int pageSize)
    {
        var specification = new GetRespondentApplicationSpecification(respondentId: respondentId, searchQuery: searchQuery, pageIndex: pageIndex, pageSize: pageSize);

        var applicationsFromDb = await applicationRepository.GetProjectRecords(specification);

        return applicationsFromDb.Adapt<IEnumerable<ApplicationResponse>>();
    }

    public async Task<ApplicationResponse> UpdateApplication(ApplicationRequest applicationRequest)
    {
        var irasApplication = applicationRequest.Adapt<ProjectRecord>();
        var respondent = applicationRequest.Respondent.Adapt<ProjectPersonnel>();

        irasApplication.ProjectPersonnelId = respondent.Id;

        var updatedApplication = await applicationRepository.UpdateProjectRecord(irasApplication);

        return updatedApplication.Adapt<ApplicationResponse>();
    }
}