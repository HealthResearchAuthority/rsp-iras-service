using Ardalis.Specification;
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
    Task<IrasApplication> GetApplication(ISpecification<IrasApplication> specification);

    /// <summary>
    /// Return all or specified number applications from the database
    /// </summary>
    Task<IEnumerable<IrasApplication>> GetApplications(ISpecification<IrasApplication> specification);

    /// <summary>
    /// Update the values of an application in the database
    /// </summary>
    /// <param name="applicationId">The application id</param>
    /// <param name="irasApplication">The application values</param>
    Task<IrasApplication> UpdateApplication(int applicationId, IrasApplication irasApplication);
}