using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Repositories;

public interface IApplicationRepository
{
    /// <summary>
    /// Adds a new application to the database
    /// </summary>
    /// <param name="irasApplication">The application values</param>
    Task<ResearchApplication> CreateApplication(ResearchApplication irasApplication);

    /// <summary>
    /// Return a single application from the database
    /// </summary>
    Task<ResearchApplication> GetApplication(ISpecification<ResearchApplication> specification);

    /// <summary>
    /// Return all or specified number applications from the database
    /// </summary>
    Task<IEnumerable<ResearchApplication>> GetApplications(ISpecification<ResearchApplication> specification);

    /// <summary>
    /// Update the values of an application in the database
    /// </summary>
    /// <param name="applicationId">The application id</param>
    /// <param name="irasApplication">The application values</param>
    Task<ResearchApplication> UpdateApplication(string applicationId, ResearchApplication irasApplication);
}