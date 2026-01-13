using Mapster;
using Rsp.Service.Application.Constants;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Settings;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Services;

public class ApplicationsService(IProjectRecordRepository applicationRepository, AppSettings appSettings) : IApplicationsService
{
    public async Task<ApplicationResponse> CreateApplication(ApplicationRequest applicationRequest)
    {
        var irasApplication = applicationRequest.Adapt<ProjectRecord>();

        var createdApplication = await applicationRepository.CreateProjectRecord(irasApplication);

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

    public async Task<PaginatedResponse<ApplicationResponse>> GetPaginatedRespondentApplications(string respondentId, ApplicationSearchRequest searchQuery, int pageIndex, int? pageSize, string? sortField, string? sortDirection)
    {
        var projectsSpecification = new GetRespondentApplicationSpecification(respondentId: respondentId, searchQuery: searchQuery);
        var projectTitleQuestionId = appSettings.QuestionIds[QuestionIdKeys.ShortProjectTitle];
        var projectTitlesSpecification = new GetRespondentProjectsTitlesSpecification(respondentId: respondentId, projectTitleQuestionId: projectTitleQuestionId);

        var (applicationsFromDb, totalCount) = await applicationRepository.GetPaginatedProjectRecords(projectsSpecification, projectTitlesSpecification, pageIndex, pageSize, sortField, sortDirection);

        return new PaginatedResponse<ApplicationResponse>
        {
            Items = applicationsFromDb.Adapt<IEnumerable<ApplicationResponse>>(),
            TotalCount = totalCount
        };
    }

    public async Task<ApplicationResponse> UpdateApplication(ApplicationRequest applicationRequest)
    {
        var irasApplication = applicationRequest.Adapt<ProjectRecord>();

        var updatedApplication = await applicationRepository.UpdateProjectRecord(irasApplication);

        return updatedApplication.Adapt<ApplicationResponse>();
    }

    /// <summary>
    /// Deletes the project record with the specified projectRecordId if it is in draft status.
    /// </summary>
    /// <param name="projectRecordId">The unique identifier of the project record to delete.</param>
    public async Task DeleteProject(string projectRecordId)
    {
        // Create a specification to find the project record with "In draft" status and the given ID.
        var specification = new GetApplicationSpecification(id: projectRecordId);

        // Delete the project record from the repository using the specification.
        await applicationRepository.DeleteProjectRecord(specification);
    }

    public async Task<PaginatedResponse<CompleteProjectRecordResponse>> GetPaginatedApplications(ProjectRecordSearchRequest searchQuery,
       int pageIndex,
       int? pageSize,
       string? sortField,
       string? sortDirection)
    {
        var (applicationsFromDb, totalCount) = await applicationRepository.GetPaginatedProjectRecords(
            searchQuery,
            pageIndex,
            pageSize,
            sortField,
            sortDirection);

        return new PaginatedResponse<CompleteProjectRecordResponse>
        {
            Items = applicationsFromDb,
            TotalCount = totalCount
        };
    }

    public async Task<ProjectRecordAuditTrailResponse> GetProjectRecordAuditTrail(string projectRecordId)
    {
        var auditTrail = await applicationRepository.GetProjectRecordAuditTrail(projectRecordId);

        return new ProjectRecordAuditTrailResponse
        {
            Items = auditTrail.Adapt<IEnumerable<ProjectRecordAuditTrailDto>>(),
            TotalCount = auditTrail.Count()
        };
    }

    public async Task UpdateProjectRecordStatus(string projectRecordId, string status)
    {
        var specification = new GetApplicationSpecification(projectRecordId);

        await applicationRepository.UpdateProjectRecordStatus(specification, status);
    }
}