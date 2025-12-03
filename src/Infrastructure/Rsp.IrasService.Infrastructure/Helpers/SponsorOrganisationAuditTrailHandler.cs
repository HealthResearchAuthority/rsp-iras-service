using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.IrasService.Domain.Attributes;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Helpers;

public class SponsorOrganisationAuditTrailHandler : IAuditTrailHandler<SponsorOrganisationAuditTrail>
{
    public bool CanHandle(object entity) => entity is SponsorOrganisation;

    public IEnumerable<SponsorOrganisationAuditTrail> GenerateAuditTrails(EntityEntry entry, string systemAdminEmail)
    {
        if (entry.Entity is not SponsorOrganisation sponsorOrganisation)
        {
            return [];
        }

        return entry.State switch
        {
            EntityState.Added => [HandleAddedState(sponsorOrganisation, systemAdminEmail)],
            EntityState.Modified => HandleModifiedState(entry, sponsorOrganisation, systemAdminEmail),
            _ => []
        };
    }

    private static SponsorOrganisationAuditTrail HandleAddedState(SponsorOrganisation sponsorOrganisation,
        string systemAdminEmail)
    {
        return new SponsorOrganisationAuditTrail
        {
            DateTimeStamp = DateTime.UtcNow,
            RtsId = sponsorOrganisation.RtsId,
            SponsorOrganisationId = sponsorOrganisation.Id,
            User = systemAdminEmail,
            Description = $"{sponsorOrganisation.RtsId} created"
        };
    }

    private static List<SponsorOrganisationAuditTrail> HandleModifiedState(EntityEntry entry,
        SponsorOrganisation sponsorOrganisation, string systemAdminEmail)
    {
        var modifiedAuditableProps = entry.Properties
            .Where(p =>
                Attribute.IsDefined(p.Metadata.PropertyInfo!, typeof(AuditableAttribute)) &&
                p.IsModified &&
                (
                    p.Metadata.ClrType != typeof(List<string>)
                        ? !Equals(p.OriginalValue, p.CurrentValue)
                        : !(p.OriginalValue as List<string> ?? new()).SequenceEqual(p.CurrentValue as List<string> ??
                            new())
                ));

        return
        [
            .. modifiedAuditableProps.Select(property => new SponsorOrganisationAuditTrail
            {
                DateTimeStamp = DateTime.UtcNow,
                RtsId = sponsorOrganisation.RtsId,
                SponsorOrganisationId = sponsorOrganisation.Id,
                User = systemAdminEmail,
                Description = GenerateDescription(property, sponsorOrganisation)
            })
        ];
    }

    private static string GenerateDescription(PropertyEntry property, SponsorOrganisation sponsorOrganisation)
    {
        var newStatus = property.CurrentValue as bool? == true ? "enabled" : "disabled";
        return $"{sponsorOrganisation.RtsId} {newStatus}";
    }
}