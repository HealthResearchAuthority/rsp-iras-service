using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Repositories;

public interface IApplicationRepository
{
    /// <summary>
    /// Adds a new application to the database
    /// </summary>
    /// <param name="irasApplication">The application values</param>
    Task<IrasApplication> CreateApplication(IrasApplication irasApplication);

    /// <summary>
    /// Return a single application from the database
    /// </summary>
    /// <param name="applicationId">The application id</param>
    Task<IrasApplication> GetApplication(int applicationId);

    /// <summary>
    /// Return all applications from the database
    /// </summary>
    Task<IEnumerable<IrasApplication>> GetApplications();

    /// <summary>
    /// Update the values of an application in the database
    /// </summary>
    /// <param name="irasApplication">The application values</param>
    /// <param name="applicationId">The application id</param>
    Task<IrasApplication> UpdateApplication(int applicationId, IrasApplication irasApplication);
}