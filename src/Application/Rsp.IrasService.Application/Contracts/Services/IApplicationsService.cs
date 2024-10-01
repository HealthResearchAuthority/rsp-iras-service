using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.Contracts.Services;

/// <summary>
/// Interface to create/read/update the application records in the database
/// </summary>
public interface IApplicationsService
{
    /// <summary>
    /// Adds a new application to the database
    /// </summary>
    /// <param name="createApplicationRequest">The application values</param>
    Task<ApplicationResponse> CreateApplication(ApplicationRequest createApplicationRequest);

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
    /// Updates the values of an application
    /// </summary>
    /// <param name="applicationRequest">The application values</param>
    Task<ApplicationResponse> UpdateApplication(ApplicationRequest applicationRequest);
}