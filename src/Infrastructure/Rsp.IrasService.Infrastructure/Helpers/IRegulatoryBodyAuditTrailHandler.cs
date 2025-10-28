using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Helpers;

public interface IRegulatoryBodyAuditTrailHandler
{
    public bool CanHandle(object entity);

    public IEnumerable<RegulatoryBodyAuditTrail> GenerateAuditTrails(EntityEntry entry, string systemAdminEmail);
}