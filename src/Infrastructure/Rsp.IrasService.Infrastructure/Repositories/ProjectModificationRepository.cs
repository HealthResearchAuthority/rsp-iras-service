using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

/// <summary>
/// Repository for managing <see cref="ProjectModification"/> and <see cref="ProjectModificationChange"/> entities in the database.
/// </summary>
public class ProjectModificationRepository(IrasContext irasContext) : IProjectModificationRepository
{
    /// <summary>
    /// Adds a new <see cref="ProjectModification"/> to the database, assigning a sequential ModificationNumber and updating the ModificationIdentifier.
    /// </summary>
    /// <param name="projectModification">The project modification entity to add.</param>
    /// <returns>The created <see cref="ProjectModification"/> entity.</returns>
    public async Task<ProjectModification> CreateModification(ProjectModification projectModification)
    {
        // Retrieve the current maximum ModificationNumber for the given ProjectRecordId.
        // This ensures that each modification for a project is sequentially numbered.
        var modificationNumber = await irasContext.ProjectModifications
            .Where(pm => pm.ProjectRecordId == projectModification.ProjectRecordId)
            .MaxAsync(pm => (int?)pm.ModificationNumber) ?? 0;

        // Increment the modification number for the new modification.
        projectModification.ModificationNumber = modificationNumber + 1;

        // Update the ModificationIdentifier to include the new ModificationNumber.
        // This typically forms a unique identifier such as "IRASID/1", "IRASID/2", etc.
        projectModification.ModificationIdentifier += projectModification.ModificationNumber;

        // Add the new ProjectModification entity to the context for tracking.
        var entity = await irasContext.ProjectModifications.AddAsync(projectModification);

        // Persist the changes to the database.
        await irasContext.SaveChangesAsync();

        // Return the newly created ProjectModification entity.
        return entity.Entity;
    }

    /// <summary>
    /// Adds a new <see cref="ProjectModificationChange"/> to the database.
    /// </summary>
    /// <param name="projectModificationChange">The project modification change entity to add.</param>
    /// <returns>The created <see cref="ProjectModificationChange"/> entity.</returns>
    public async Task<ProjectModificationChange> CreateModificationChange(ProjectModificationChange projectModificationChange)
    {
        var entity = await irasContext.ProjectModificationChanges.AddAsync(projectModificationChange);

        await irasContext.SaveChangesAsync();

        return entity.Entity;
    }
}