using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.IrasService.Domain.Attributes;
using Rsp.IrasService.Domain.Constants;
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
            EntityState.Added => HandleAddedState(sponsorOrganisationUser, systemAdminEmail),
            EntityState.Modified => HandleModifiedState(entry, sponsorOrganisationUser, systemAdminEmail),
            _ => []
        };
    }

    private static List<SponsorOrganisationAuditTrail> HandleAddedState(SponsorOrganisationUser sponsorOrganisationUser,
        string systemAdminEmail)
    {
        var result = new List<SponsorOrganisationAuditTrail>
        {
            new() {
                DateTimeStamp = DateTime.UtcNow,
                RtsId = sponsorOrganisationUser.RtsId,
                User = systemAdminEmail,
                Description = $"{sponsorOrganisationUser.Email} added to sponsor organisation"
            }
        };

        if (sponsorOrganisationUser.SponsorRole is not null)
        {
            result.Add(new SponsorOrganisationAuditTrail
            {
                DateTimeStamp = DateTime.UtcNow,
                RtsId = sponsorOrganisationUser.RtsId,
                User = systemAdminEmail,
                Description = $"{sponsorOrganisationUser.Email} assigned {sponsorOrganisationUser.SponsorRole} for sponsor organisation"
            });

            if (sponsorOrganisationUser.SponsorRole != Roles.OrganisationAdministrator && sponsorOrganisationUser.IsAuthoriser)
            {
                result.Add(new SponsorOrganisationAuditTrail
                {
                    DateTimeStamp = DateTime.UtcNow,
                    RtsId = sponsorOrganisationUser.RtsId,
                    User = systemAdminEmail,
                    Description = $"{sponsorOrganisationUser.Email} assigned as authoriser for sponsor organisation"
                });
            }
        }

        return result;
    }

    private static List<SponsorOrganisationAuditTrail> HandleModifiedState
    (
        EntityEntry entry,
        SponsorOrganisationUser sponsorOrganisationUser,
        string systemAdminEmail
    )
    {
        var modifiedAuditableProps = entry.Properties
            .Where(p =>
                Attribute.IsDefined(p.Metadata.PropertyInfo!, typeof(AuditableAttribute)) &&
                p.IsModified &&
                (
                    p.Metadata.ClrType != typeof(List<string>)
                        ? !Equals(p.OriginalValue, p.CurrentValue)
                        : !(p.OriginalValue as List<string> ?? []).SequenceEqual(p.CurrentValue as List<string> ?? [])
                ));

        var auditTrails = new List<SponsorOrganisationAuditTrail>();

        foreach (var modifiedProperty in modifiedAuditableProps)
        {
            var auditTrail = (modifiedProperty.Metadata.Name) switch
            {
                nameof(SponsorOrganisationUser.IsActive) =>
                    [GenerateStatusChangeAuditTrail(modifiedProperty, sponsorOrganisationUser, systemAdminEmail)],
                nameof(SponsorOrganisationUser.SponsorRole) =>
                    GenerateRoleChangeDescription(modifiedProperty, sponsorOrganisationUser, systemAdminEmail),
                nameof(SponsorOrganisationUser.IsAuthoriser) =>
                    [GenerateAuthoriserChangeDescription(modifiedProperty, sponsorOrganisationUser, systemAdminEmail)],
                _ => null
            };

            if (auditTrail is not null)
            {
                auditTrails.AddRange(auditTrail);
            }
        }

        return auditTrails;
    }

    private static SponsorOrganisationAuditTrail GenerateAuthoriserChangeDescription(PropertyEntry entry, SponsorOrganisationUser sponsorOrganisationUser, string systemAdminEmail)
    {
        var newStatus = entry.CurrentValue as bool? == true ? "assigned as" : "unassigned";

        return new SponsorOrganisationAuditTrail
        {
            DateTimeStamp = DateTime.UtcNow,
            RtsId = sponsorOrganisationUser.RtsId,
            SponsorOrganisationId = sponsorOrganisationUser.Id,
            User = systemAdminEmail,
            Description = $"{sponsorOrganisationUser.Email} {newStatus} authoriser for sponsor organisation"
        };
    }

    private static List<SponsorOrganisationAuditTrail> GenerateRoleChangeDescription(PropertyEntry entry, SponsorOrganisationUser sponsorOrganisationUser, string systemAdminEmail)
    {
        var oldRole = entry.OriginalValue as string;
        var newRole = entry.CurrentValue as string;

        return
        [
            new()
            {
                DateTimeStamp = DateTime.UtcNow,
                RtsId = sponsorOrganisationUser.RtsId,
                SponsorOrganisationId = sponsorOrganisationUser.Id,
                User = systemAdminEmail,
                Description = $"{sponsorOrganisationUser.Email} unassigned {oldRole} for sponsor organisation"
            },
            new()
            {
                DateTimeStamp = DateTime.UtcNow,
                RtsId = sponsorOrganisationUser.RtsId,
                SponsorOrganisationId = sponsorOrganisationUser.Id,
                User = systemAdminEmail,
                Description = $"{sponsorOrganisationUser.Email} assigned {newRole} for sponsor organisation"
            }
        ];
    }

    private static SponsorOrganisationAuditTrail GenerateStatusChangeAuditTrail(PropertyEntry entry, SponsorOrganisationUser sponsorOrganisationUser, string systemAdminEmail)
    {
        var newStatus = entry.CurrentValue as bool? == true ? "enabled for" : "disabled from";

        return new SponsorOrganisationAuditTrail
        {
            DateTimeStamp = DateTime.UtcNow,
            RtsId = sponsorOrganisationUser.RtsId,
            SponsorOrganisationId = sponsorOrganisationUser.Id,
            User = systemAdminEmail,
            Description = $"{sponsorOrganisationUser.Email} {newStatus} sponsor organisation"
        };
    }
}