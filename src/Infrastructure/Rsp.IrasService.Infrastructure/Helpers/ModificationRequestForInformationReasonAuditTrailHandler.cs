using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.Helpers;

[ExcludeFromCodeCoverage]
public class ModificationRequestForInformationReasonAuditTrailHandler : IAuditTrailHandler<ProjectModificationAuditTrail>
{
    public bool CanHandle(object entity) => entity is ModificationRfiReason;

    public IEnumerable<ProjectModificationAuditTrail> GenerateAuditTrails(EntityEntry entry, string userEmail)
    {
        if (entry.Entity is not ModificationRfiReason record)
        {
            return [];
        }

        var reasonProperty = entry.Property(nameof(ModificationRfiReason.Reason));
        var originalReason = reasonProperty.OriginalValue?.ToString() ?? "(null)";
        var currentReason = reasonProperty.CurrentValue?.ToString() ?? "(null)";

        if (entry.State == EntityState.Modified && originalReason == currentReason)
        {
            return [];
        }

        return entry.State switch
        {
            EntityState.Added => [
                new ProjectModificationAuditTrail
                {
                    DateTimeStamp = DateTime.UtcNow,
                    ProjectModificationId = record.ProjectModificationId,
                    User = userEmail,
                    Description = $"Reason {record.Sequence} for Request for further information added: '{currentReason}'",
                    IsBackstageOnly = true,
                    ShowUserEmailToFrontstage = false
                }
            ],
            EntityState.Modified => [
                new ProjectModificationAuditTrail
                {
                    DateTimeStamp = DateTime.UtcNow,
                    ProjectModificationId = record.ProjectModificationId,
                    User = userEmail,
                    Description = $"Reason {record.Sequence} for Request for further information changed from '{originalReason}' to '{currentReason}'",
                    IsBackstageOnly = true,
                    ShowUserEmailToFrontstage = false
                }
            ],
            EntityState.Deleted => [
                new ProjectModificationAuditTrail
                {
                    DateTimeStamp = DateTime.UtcNow,
                    ProjectModificationId = record.ProjectModificationId,
                    User = userEmail,
                    Description = $"Reason {record.Sequence} for Request for further information removed",
                    IsBackstageOnly = true,
                    ShowUserEmailToFrontstage = false
                }
            ],
            _ => []
        };
    }
}