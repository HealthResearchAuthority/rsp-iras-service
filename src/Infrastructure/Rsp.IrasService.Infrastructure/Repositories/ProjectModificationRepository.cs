using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class ProjectModificationRepository(IrasContext irasContext) : IProjectModificationRepository
{
    public async Task<ProjectModification> CreateModification(ProjectModification projectModification)
    {
        // Get current max ModificationNumber for this ProjectRecordId
        var modificationNumber = await irasContext.ProjectModifications
            .Where(pm => pm.ProjectRecordId == projectModification.ProjectRecordId)
            .MaxAsync(pm => (int?)pm.ModificationNumber) ?? 0;

        projectModification.ModificationNumber = modificationNumber + 1;
        projectModification.ModificationIdentifier += projectModification.ModificationNumber;

        var entity = await irasContext.ProjectModifications.AddAsync(projectModification);

        await irasContext.SaveChangesAsync();

        return entity.Entity;
    }

    public async Task<ProjectModificationChange> CreateModificationChange(ProjectModificationChange projectModificationChange)
    {
        var entity = await irasContext.ProjectModificationChanges.AddAsync(projectModificationChange);

        await irasContext.SaveChangesAsync();

        return entity.Entity;
    }
}