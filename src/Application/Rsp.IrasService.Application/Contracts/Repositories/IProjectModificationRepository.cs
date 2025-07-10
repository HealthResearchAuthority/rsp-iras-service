using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IProjectModificationRepository
{
    /// <summary>
    /// Adds a new ProjectRecord to the database
    /// </summary>
    /// <param name="projectModification">The application values</param>
    Task<ProjectModification> CreateModification(ProjectModification projectModification);

    Task<ProjectModificationChange> CreateModificationChange(ProjectModificationChange projectModificationChange);
}