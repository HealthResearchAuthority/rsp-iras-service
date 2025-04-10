﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.IrasService.Domain.Attributes;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Helpers;

public class ReviewBodyAuditTrailHandler : IAuditTrailHandler
{
    public bool CanHandle(object entity) => entity is ReviewBody;

    public IEnumerable<ReviewBodyAuditTrail> GenerateAuditTrails(EntityEntry entry, string systemAdminEmail)
    {
        if (entry.Entity is not ReviewBody reviewBody)
        {
            return [];
        }

        return entry.State switch
        {
            EntityState.Added => [HandleAddedState(reviewBody, systemAdminEmail)],
            EntityState.Modified => HandleModifiedState(entry, reviewBody, systemAdminEmail),
            _ => []
        };
    }

    private static ReviewBodyAuditTrail HandleAddedState(ReviewBody reviewBody, string systemAdminEmail)
    {
        return new ReviewBodyAuditTrail
        {
            DateTimeStamp = DateTime.UtcNow,
            ReviewBodyId = reviewBody.Id,
            User = systemAdminEmail,
            Description = $"{reviewBody.OrganisationName} was created"
        };
    }

    private static List<ReviewBodyAuditTrail> HandleModifiedState(EntityEntry entry, ReviewBody reviewBody, string systemAdminEmail)
    {
        var modifiedAuditableProps = entry.Properties
            .Where(p =>
                Attribute.IsDefined(p.Metadata.PropertyInfo!, typeof(AuditableAttribute)) &&
                (p.Metadata.ClrType != typeof(List<string>)
                    ? !Equals(p.OriginalValue, p.CurrentValue)
                    : !AreListsEqual(p.OriginalValue as List<string>, p.CurrentValue as List<string>)) &&
                p.IsModified);

        return [.. modifiedAuditableProps.Select(property => new ReviewBodyAuditTrail
        {
            DateTimeStamp = DateTime.UtcNow,
            ReviewBodyId = reviewBody.Id,
            User = systemAdminEmail,
            Description = GenerateDescription(property, reviewBody)
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

    private static string GenerateDescription(PropertyEntry property, ReviewBody reviewBody)
    {
        if (property.Metadata.Name == nameof(ReviewBody.IsActive))
        {
            var newStatus = property.CurrentValue as bool? == true ? "enabled" : "disabled";
            return $"{reviewBody.OrganisationName} was {newStatus}";
        }
        else
        {
            const string emptyValue = "(null)"; // business is yet to decide how to handle this case, using '(null)' for now

            var oldValue = property.OriginalValue ?? emptyValue;
            var newValue = property.CurrentValue ?? emptyValue;

            if (property.Metadata.Name == nameof(ReviewBody.Countries))
            {
                oldValue = string.Join(", ", property.OriginalValue as List<string> ?? [emptyValue]);
                newValue = string.Join(", ", property.CurrentValue as List<string> ?? [emptyValue]);
            }

            return $"{property.Metadata.Name} was changed from {oldValue} to {newValue}";
        }
    }
}