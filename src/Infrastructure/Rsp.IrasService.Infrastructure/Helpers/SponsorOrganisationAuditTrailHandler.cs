using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.IrasService.Domain.Attributes;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Helpers;

public class SponsorOrganisationAuditTrailHandler : ISponsorOrganisationAuditTrailHandler
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

    private static SponsorOrganisationAuditTrail HandleAddedState(SponsorOrganisation sponsorOrganisation, string systemAdminEmail)
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

    private static List<SponsorOrganisationAuditTrail> HandleModifiedState(EntityEntry entry, SponsorOrganisation sponsorOrganisation, string systemAdminEmail)
    {
        var modifiedAuditableProps = entry.Properties
            .Where(p =>
                Attribute.IsDefined(p.Metadata.PropertyInfo!, typeof(AuditableAttribute)) &&
                (p.Metadata.ClrType != typeof(List<string>)
                    ? !Equals(p.OriginalValue, p.CurrentValue)
                    : !AreListsEqual(p.OriginalValue as List<string>, p.CurrentValue as List<string>)) &&
                p.IsModified);

        return [.. modifiedAuditableProps.Select(property => new SponsorOrganisationAuditTrail
        {
            DateTimeStamp = DateTime.UtcNow,
            RtsId = sponsorOrganisation.RtsId,
            SponsorOrganisationId = sponsorOrganisation.Id,
            User = systemAdminEmail,
            Description = GenerateDescription(property, sponsorOrganisation)
        })];
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

    private static string GenerateDescription(PropertyEntry property, SponsorOrganisation sponsorOrganisation)
    {
        if (property.Metadata.Name == nameof(SponsorOrganisation.IsActive))
        {
            var newStatus = property.CurrentValue as bool? == true ? "enabled" : "disabled";
            return $"{sponsorOrganisation.RtsId} was {newStatus}";
        }
        else
        {
            const string emptyValue = "(null)"; // business is yet to decide how to handle this case, using '(null)' for now

            var oldValue = property.OriginalValue ?? emptyValue;
            var newValue = property.CurrentValue ?? emptyValue;

          

            return $"{property.Metadata.Name} was changed from {oldValue} to {newValue}";
        }
    }
}