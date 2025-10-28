using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Domain.Interfaces;
using Rsp.IrasService.Infrastructure.Helpers;

namespace Rsp.IrasService.Infrastructure.Interceptors;

[ExcludeFromCodeCoverage]
public class RegulatoryBodyAuditTrailInterceptor(IAuditTrailDetailsService auditTrailDetailsService, IEnumerable<IRegulatoryBodyAuditTrailHandler> auditTrailHandlers) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync
    (
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        if (eventData.Context is not IrasContext dbContext)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var auditableEntries = dbContext
            .ChangeTracker
            .Entries<IAuditable>();

        if (!auditableEntries.Any())
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var systemAdminEmail = auditTrailDetailsService.GetEmailFromHttpContext();

        var auditTrailRecords = new List<RegulatoryBodyAuditTrail>();

        foreach (var entry in auditableEntries)
        {
            var validHandlers = auditTrailHandlers
                .Where(auditTrailHandler => auditTrailHandler.CanHandle(entry.Entity))
                .ToList();

            foreach (var handler in validHandlers)
            {
                auditTrailRecords.AddRange(handler.GenerateAuditTrails(entry, systemAdminEmail));
            }
        }

        await dbContext.RegulatoryBodiesAuditTrail.AddRangeAsync(auditTrailRecords, cancellationToken);

        return await new ValueTask<InterceptionResult<int>>(result);
    }
}