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
            Description = $"{sponsorOrganisationUser.Email} was added to this sponsor organisation"
        };
    }

    private static List<SponsorOrganisationAuditTrail> HandleModifiedState(EntityEntry entry,
        SponsorOrganisationUser sponsorOrganisationUser, string systemAdminEmail)
    {
        var modifiedAuditableProps = entry.Properties
            .Where(p =>
                Attribute.IsDefined(p.Metadata.PropertyInfo!, typeof(AuditableAttribute)) &&
                (p.Metadata.ClrType != typeof(List<string>)
                    ? !Equals(p.OriginalValue, p.CurrentValue)
                    : !AreListsEqual(p.OriginalValue as List<string>, p.CurrentValue as List<string>)) &&
                p.IsModified);

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

    private static bool AreListsEqual(List<string>? list1, List<string>? list2)
    {
        if (list1 == null && list2 == null)
        {
            return true;
        }

        if (list1 == null || list2 == null)
        {
            return false;
        }

        if (list1.Count != list2.Count)
        {
            return false;
        }

        return list1.SequenceEqual(list2);
    }

    private static string GenerateDescription(PropertyEntry property, SponsorOrganisationUser sponsorOrganisationUser)
    {
        var newStatus = property.CurrentValue as bool? == true ? "enabled" : "disabled";
        return $"{sponsorOrganisationUser.Email} {newStatus} for {sponsorOrganisationUser.RtsId}";
    }
}