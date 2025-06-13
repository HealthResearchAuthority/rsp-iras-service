using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IApplicationRepository
{
    /// <summary>
    /// Adds a new application to the database
    /// </summary>
    /// <param name="irasApplication">The application values</param>
    Task<ProjectApplication> CreateApplication(ProjectApplication irasApplication, ProjectApplicationRespondent respondent);

    /// <summary>
    /// Return a single application from the database
    /// </summary>
    Task<ProjectApplication?> GetApplication(ISpecification<ProjectApplication> specification);

    /// <summary>
    /// Return all or specified number applications from the database
    /// </summary>
    Task<IEnumerable<ProjectApplication>> GetApplications(ISpecification<ProjectApplication> specification);

    /// <summary>
    /// Update the values of an application in the database
    /// </summary>
    /// <param name="irasApplication">The application values</param>
    Task<ProjectApplication?> UpdateApplication(ProjectApplication irasApplication);
}