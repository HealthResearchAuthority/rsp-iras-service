using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Helpers;

public interface ISponsorOrganisationAuditTrailHandler
{
    public bool CanHandle(object entity);

    public IEnumerable<SponsorOrganisationAuditTrail> GenerateAuditTrails(EntityEntry entry, string systemAdminEmail);
}