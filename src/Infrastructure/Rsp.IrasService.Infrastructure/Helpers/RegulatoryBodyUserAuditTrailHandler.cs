using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Helpers;

[ExcludeFromCodeCoverage]
public class RegulatoryBodyUserAuditTrailHandler : IRegulatoryBodyAuditTrailHandler
{
    public bool CanHandle(object entity) => entity is RegulatoryBodyUser;

    public IEnumerable<RegulatoryBodyAuditTrail> GenerateAuditTrails(EntityEntry entry, string systemAdminEmail)
    {
        if (entry.Entity is not RegulatoryBodyUser regulatoryBodyUser)
        {
            return [];
        }

        return entry.State switch
        {
            EntityState.Added =>
            [
                new RegulatoryBodyAuditTrail
                {
                    DateTimeStamp = DateTime.UtcNow,
                    RegulatoryBodyId = regulatoryBodyUser.Id,
                    User = systemAdminEmail,
                    Description = $"{regulatoryBodyUser.Email} was added to this review body"
                }
            ],
            EntityState.Deleted =>
            [
                new RegulatoryBodyAuditTrail
                {
                    DateTimeStamp = DateTime.UtcNow,
                    RegulatoryBodyId = regulatoryBodyUser.Id,
                    User = systemAdminEmail,
                    Description = $"{regulatoryBodyUser.Email} was removed from this review body"
                }
            ],
            _ => []
        };
    }
}