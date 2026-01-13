using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Rsp.Service.Infrastructure.Helpers;

public interface IAuditTrailHandler<out TAudit> where TAudit : class
{
    bool CanHandle(object entity);

    IEnumerable<TAudit> GenerateAuditTrails(EntityEntry entry, string systemAdminEmail);
}