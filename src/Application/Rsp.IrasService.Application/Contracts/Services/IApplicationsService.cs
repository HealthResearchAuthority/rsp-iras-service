using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.Logging.Interceptors;

namespace Rsp.IrasService.Application.Contracts.Services;

/// <summary>
/// Interface to create/read/update the application records in the database. Marked as IInterceptable to enable
/// the start/end logging for all methods.
/// </summary>
public interface IApplicationsService : IInterceptable
{
    /// <summary>
    /// Adds a new application to the database
    /// </summary>
    /// <param name="applicationRequest">The application values</param>
    Task<ApplicationResponse> CreateApplication(ApplicationRequest applicationRequest);

    /// <summary>
    /// Returns a single application
    /// </summary>
    /// <param name="applicationId">Id of the application</param>
    Task<ApplicationResponse> GetApplication(string applicationId);

    /// <summary>
    /// Returns a single application with the specified status
    /// </summary>
    /// <param name="applicationId">Id of the application</param>
    Task<ApplicationResponse> GetApplication(string applicationId, string applicationStatus);

    /// <summary>
    /// Returns all applications
    /// </summary>
    Task<IEnumerable<ApplicationResponse>> GetApplications();

    /// <summary>
    /// Returns all applications with specified status
    /// </summary>
    Task<IEnumerable<ApplicationResponse>> GetApplications(string applicationStatus);

    /// <summary>
    /// Returns all applications for a specified respondent
    /// </summary>
    Task<IEnumerable<ApplicationResponse>> GetRespondentApplications(string respondentId);

    /// <summary>
    /// Returns applications for a specified respondent with pagination
    /// </summary>
    Task<PaginatedResponse<ApplicationResponse>> GetPaginatedRespondentApplications(string respondentId, ApplicationSearchRequest searchQuery, int pageIndex, int? pageSize, string? sortField, string? sortDirection);

    /// <summary>
    /// Updates the values of an application
    /// </summary>
    /// <param name="applicationRequest">The application values</param>
    Task<ApplicationResponse> UpdateApplication(ApplicationRequest applicationRequest);

    /// <summary>
    /// Deletes the project with the specified projectRecordId
    /// </summary>
    /// <param name="projectRecordId">The project record id</param>
    Task DeleteProject(string projectRecordId);

    Task<PaginatedResponse<CompleteProjectRecordResponse>> GetPaginatedApplications(ProjectRecordSearchRequest searchQuery,
        int pageIndex,
        int? pageSize,
        string? sortField,
        string? sortDirection);
}