using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts;

/// <summary>
/// Interface to create/read/update the application records in the database
/// </summary>
public interface IApplicationsService
{
    /// <summary>
    /// Adds a new application to the database
    /// </summary>
    /// <param name="irasApplication">The application values</param>
    Task CreateApplication(IrasApplication irasApplication);

    /// <summary>
    /// Returns an application
    /// </summary>
    /// <param name="applicationId">Id of the application</param>
    Task<IrasApplication> GetApplication(int applicationId);

    Task<IEnumerable<IrasApplication>> GetApplications();

    /// <summary>
    /// Updates the values of an application
    /// </summary>
    /// <param name="applicationId">Id of the application</param>
    /// <param name="irasApplication">The application values</param>
    Task UpdateApplication(int applicationId, IrasApplication irasApplication);
}