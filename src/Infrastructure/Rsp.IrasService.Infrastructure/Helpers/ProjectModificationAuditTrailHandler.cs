using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.IrasService.Application.Constants;
using Rsp.IrasService.Domain.Attributes;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Helpers;

[ExcludeFromCodeCoverage]
public class ProjectModificationAuditTrailHandler :
    IAuditTrailHandler<ProjectModificationAuditTrail>
{
    public bool CanHandle(object entity) => entity is ProjectModification;

    public IEnumerable<ProjectModificationAuditTrail> GenerateAuditTrails
    (
        EntityEntry entry,
        string userEmail
    )
    {
        if (entry.Entity is not ProjectModification projectModification)
        {
            return [];
        }

        return entry.State switch
        {
            EntityState.Added =>
                [HandleAddedState(projectModification, userEmail)],
            EntityState.Modified =>
                HandleModifiedState(entry, projectModification, userEmail),
            _ => []
        };
    }

    private static ProjectModificationAuditTrail HandleAddedState
    (
        ProjectModification projectModification,
        string userEmail
    )
    {
        return new ProjectModificationAuditTrail
        {
            DateTimeStamp = DateTime.UtcNow,
            ProjectModificationId = projectModification.Id,
            User = userEmail,
            Description = $"Modification created"
        };
    }

    private static List<ProjectModificationAuditTrail> HandleModifiedState
    (
        EntityEntry entry,
        ProjectModification projectModification,
        string userEmail
    )
    {
        var modifiedAuditableProps = entry.Properties
            .Where(p =>
                Attribute.IsDefined
                    (p.Metadata.PropertyInfo!, typeof(AuditableAttribute)) &&
                !Equals(p.OriginalValue, p.CurrentValue)
            );

        var result = new List<ProjectModificationAuditTrail>();

        foreach (var p in modifiedAuditableProps)
        {
            var auditTrailRecord = new ProjectModificationAuditTrail
            {
                DateTimeStamp = DateTime.UtcNow,
                ProjectModificationId = projectModification.Id,
                User = userEmail,
                Description = p.Metadata.Name switch
                {
                    nameof(projectModification.Status) =>
                        GenerateStatusChangeDescription(p.CurrentValue!.ToString()!),
                    nameof(projectModification.ReviewerEmail) =>
                        GenerateReviewerChangeDescription(p.CurrentValue!.ToString()!, p.OriginalValue?.ToString()),
                    _ =>
                        $"{p.Metadata.Name} changed from '{p.OriginalValue}' to '{p.CurrentValue}'",
                }
            };

            if (!string.IsNullOrEmpty(auditTrailRecord.Description))
            {
                result.Add(auditTrailRecord);
            }
        }

        return result;
    }

    private static string GenerateStatusChangeDescription(string newStatus)
    {
        return newStatus switch
        {
            ModificationStatus.WithSponsor => "Modification sent to sponsor",
            ModificationStatus.WithReviewBody => "Modification authorised by sponsor",
            ModificationStatus.Approved => "Modification approved by review body",
            ModificationStatus.NotApproved => "Modification not approved by review body",
            ModificationStatus.NotAuthorised => "Modification not authorised by sponsor",
            _ => "",
        };
    }

    private static string GenerateReviewerChangeDescription(string newReviewerEmail, string? oldReviewerEmail)
    {
        return oldReviewerEmail == null ?
            $"Modification assigned to '{newReviewerEmail}'" :
            $"Modification reassigned to '{newReviewerEmail}'";
    }
}