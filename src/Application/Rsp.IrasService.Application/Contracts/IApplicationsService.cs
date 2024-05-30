using Rsp.IrasService.Application.Requests;
using Rsp.IrasService.Application.Responses;

namespace Rsp.IrasService.Application.Contracts;

/// <summary>
/// Interface to create/read/update the application records in the database
/// </summary>
public interface IApplicationsService
{
    /// <summary>
    /// Adds a new application to the database
    /// </summary>
    /// <param name="createApplicationRequest">The application values</param>
    Task<CreateApplicationResponse> CreateApplication(CreateApplicationRequest createApplicationRequest);

    /// <summary>
    /// Returns a single application
    /// </summary>
    /// <param name="applicationId">Id of the application</param>
    Task<GetApplicationResponse> GetApplication(int applicationId);

    /// <summary>
    /// Returns a single application with the specified status
    /// </summary>
    /// <param name="applicationId">Id of the application</param>
    Task<GetApplicationResponse> GetApplication(int applicationId, string applicationStatus);

    /// <summary>
    /// Returns all applications
    /// </summary>
    Task<IEnumerable<GetApplicationResponse>> GetApplications();

    /// <summary>
    /// Returns all applications with specified status
    /// </summary>
    Task<IEnumerable<GetApplicationResponse>> GetApplications(string applicationStatus);

    /// <summary>
    /// Updates the values of an application
    /// </summary>
    /// <param name="applicationId">Id of the application</param>
    /// <param name="createApplicationRequest">The application values</param>
    Task<CreateApplicationResponse> UpdateApplication(int applicationId, CreateApplicationRequest createApplicationRequest);
}