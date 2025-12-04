using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.IrasService.Domain.Attributes;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Helpers;

[ExcludeFromCodeCoverage]
public class SponsorOrganisationUserAuditTrailHandler : IAuditTrailHandler<SponsorOrganisationAuditTrail>
{
    public bool CanHandle(object entity) => entity is SponsorOrganisationUser;

    public IEnumerable<SponsorOrganisationAuditTrail> GenerateAuditTrails(EntityEntry entry, string systemAdminEmail)
    {
        if (entry.Entity is not SponsorOrganisationUser sponsorOrganisationUser)
        {
            return [];
        }

        return entry.State switch
        {
            EntityState.Added => [HandleAddedState(sponsorOrganisationUser, systemAdminEmail)],
            EntityState.Modified => HandleModifiedState(entry, sponsorOrganisationUser, systemAdminEmail),
            _ => []
        };
    }

    private static SponsorOrganisationAuditTrail HandleAddedState(SponsorOrganisationUser sponsorOrganisationUser,
        string systemAdminEmail)
    {
        return new SponsorOrganisationAuditTrail
        {
            DateTimeStamp = DateTime.UtcNow,
            RtsId = sponsorOrganisationUser.RtsId,
            User = systemAdminEmail,
            Description = $"{sponsorOrganisationUser.Email} added to {sponsorOrganisationUser.RtsId}"
        };
    }

    private static List<SponsorOrganisationAuditTrail> HandleModifiedState(EntityEntry entry,
        SponsorOrganisationUser sponsorOrganisationUser, string systemAdminEmail)
    {
        var modifiedAuditableProps = entry.Properties
            .Where(p =>
                Attribute.IsDefined(p.Metadata.PropertyInfo!, typeof(AuditableAttribute)) &&
                p.IsModified &&
                (
                    p.Metadata.ClrType != typeof(List<string>)
                        ? !Equals(p.OriginalValue, p.CurrentValue)
                        : !(p.OriginalValue as List<string> ?? new()).SequenceEqual(p.CurrentValue as List<string> ?? new())
                ));

        return
        [
            .. modifiedAuditableProps.Select(property => new SponsorOrganisationAuditTrail
            {
                DateTimeStamp = DateTime.UtcNow,
                RtsId = sponsorOrganisationUser.RtsId,
                SponsorOrganisationId = sponsorOrganisationUser.Id,
                User = systemAdminEmail,
                Description = GenerateDescription(property, sponsorOrganisationUser)
            })
        ];
    }

    private static string GenerateDescription(PropertyEntry property, SponsorOrganisationUser sponsorOrganisationUser)
    {
        var newStatus = property.CurrentValue as bool? == true ? "enabled for" : "disabled from";
        return $"{sponsorOrganisationUser.Email} {newStatus} {sponsorOrganisationUser.RtsId}";
    }
}