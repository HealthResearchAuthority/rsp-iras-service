using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Domain.Interfaces;
using Rsp.IrasService.Infrastructure.Helpers;

namespace Rsp.IrasService.Infrastructure.Interceptors;

[ExcludeFromCodeCoverage]
public class AuditTrailInterceptor(
    IAuditTrailDetailsService auditTrailDetailsService,
    IEnumerable<IAuditTrailHandler<SponsorOrganisationAuditTrail>> sponsorHandlers,
    IEnumerable<IAuditTrailHandler<RegulatoryBodyAuditTrail>> regulatoryHandlers
) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not DbContext db)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var auditableEntries = db.ChangeTracker.Entries()
                                 .Where(e => e.Entity is IAuditable)
                                 .ToList();
        if (auditableEntries.Count == 0)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var userEmail = auditTrailDetailsService.GetEmailFromHttpContext();

        // Collect both audit types via a single generic routine
        var sponsorRecords = Collect(sponsorHandlers, auditableEntries, userEmail);
        var regulatoryRecords = Collect(regulatoryHandlers, auditableEntries, userEmail);

        if (sponsorRecords.Count > 0)
            await db.Set<SponsorOrganisationAuditTrail>().AddRangeAsync(sponsorRecords, cancellationToken);

        if (regulatoryRecords.Count > 0)
            await db.Set<RegulatoryBodyAuditTrail>().AddRangeAsync(regulatoryRecords, cancellationToken);

        return result;
    }

    private static List<TAudit> Collect<TAudit>(
        IEnumerable<IAuditTrailHandler<TAudit>> handlers,
        List<EntityEntry> entries,
        string userEmail)
        where TAudit : class
    {
        return entries
            .SelectMany(entry =>
                handlers
                    .Where(h => h.CanHandle(entry.Entity))
                    .SelectMany(h => h.GenerateAuditTrails(entry, userEmail))
            )
            .ToList();
    }
}
