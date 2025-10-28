using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Helpers;

public interface IAuditTrailHandler<TAudit> where TAudit : class
{
    bool CanHandle(object entity);
    IEnumerable<TAudit> GenerateAuditTrails(EntityEntry entry, string userEmail);
}