using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IProjectClosureRepository
{
    /// <summary>
    /// Add new project closure record to database
    /// </summary>
    /// <param name="projectClosure"></param>
    /// <returns></returns>
    Task<ProjectClosure> CreateProjectClosure(ProjectClosure projectClosure);

    /// <summary>
    /// Returns the singale projcect closure record from database
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    Task<ProjectClosure?> GetProjectClosure(ISpecification<ProjectClosure> specification);

    /// <summary>
    /// Update the projectClosure records
    /// </summary>
    /// <param name="projectClosure"></param>
    /// <returns></returns>
    Task<ProjectClosure> UpdateProjectClosureStatus(ProjectClosure projectClosure);
}