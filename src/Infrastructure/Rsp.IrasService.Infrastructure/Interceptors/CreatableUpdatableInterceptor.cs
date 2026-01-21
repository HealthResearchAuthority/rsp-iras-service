using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Rsp.Service.Domain.Interfaces;
using Rsp.Service.Infrastructure.Helpers;

namespace Rsp.Service.Infrastructure.Interceptors;

public class CreatableUpdatableInterceptor(IContextAccessorService contextAccessorService) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync
    (
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        if (eventData.Context is not DbContext db)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var entries = db.ChangeTracker.Entries()
            .Where(e => e.Entity is ICreatable or IUpdatable)
            .ToList();

        if (entries.Count == 0)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var userId = contextAccessorService.GetUserId();

        foreach (var entry in entries)
        {
            if (entry.Entity is ICreatable createdEntity && entry.State == EntityState.Added)
            {
                createdEntity.CreatedBy = userId;
                createdEntity.CreatedDate = DateTime.UtcNow;
            }

            if (entry.Entity is IUpdatable updatedEntity && (entry.State == EntityState.Modified || entry.State == EntityState.Added))
            {
                updatedEntity.UpdatedBy = userId;
                updatedEntity.UpdatedDate = DateTime.UtcNow;
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}