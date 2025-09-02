using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

/// <summary>
/// Defines methods for managing project modifications and their changes in the data store.
/// </summary>
public interface IProjectModificationRepository
{
    /// <summary>
    /// Adds a new <see cref="ProjectModification"/> to the database.
    /// </summary>
    /// <param name="projectModification">The project modification entity to add.</param>
    /// <returns>The created <see cref="ProjectModification"/> entity.</returns>
    Task<ProjectModification> CreateModification(ProjectModification projectModification);

    /// <summary>
    /// Adds a new <see cref="ProjectModificationChange"/> to the database.
    /// </summary>
    /// <param name="projectModificationChange">The project modification change entity to add.</param>
    /// <returns>The created <see cref="ProjectModificationChange"/> entity.</returns>
    Task<ProjectModificationChange> CreateModificationChange(ProjectModificationChange projectModificationChange);

    IEnumerable<ProjectModificationResult> GetModifications
    (
        ModificationSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        string? projectRecordId = null
    );

    int GetModificationsCount(ModificationSearchRequest searchQuery, string? projectRecordId = null);
}