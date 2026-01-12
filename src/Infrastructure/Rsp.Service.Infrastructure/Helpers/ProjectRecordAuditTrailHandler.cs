using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.Service.Application.Constants;
using Rsp.Service.Domain.Attributes;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.Helpers;

[ExcludeFromCodeCoverage]
public class ProjectRecordAuditTrailHandler : IAuditTrailHandler<ProjectRecordAuditTrail>
{
    public bool CanHandle(object entity) => entity is ProjectRecord;

    public IEnumerable<ProjectRecordAuditTrail> GenerateAuditTrails(EntityEntry entry, string userEmail)
    {
        if (entry.Entity is not ProjectRecord projectRecord)
        {
            return [];
        }

        return entry.State switch
        {
            EntityState.Added => [HandleAddedState(projectRecord, userEmail)],
            EntityState.Modified => HandleModifiedState(entry, projectRecord, userEmail),
            _ => []
        };
    }

    private static ProjectRecordAuditTrail HandleAddedState(ProjectRecord reviewBody, string userEmail)
    {
        return new ProjectRecordAuditTrail
        {
            DateTimeStamp = DateTime.UtcNow,
            ProjectRecordId = reviewBody.Id,
            User = userEmail,
            Description = "Project record draft started"
        };
    }

    private static List<ProjectRecordAuditTrail> HandleModifiedState(EntityEntry entry, ProjectRecord projectRecord, string userEmail)
    {
        var modifiedAuditableProps = entry.Properties
            .Where(p =>
                Attribute.IsDefined(p.Metadata.PropertyInfo!, typeof(AuditableAttribute)) &&
                !Equals(p.OriginalValue, p.CurrentValue));

        var result = new List<ProjectRecordAuditTrail>();

        foreach (var p in modifiedAuditableProps)
        {
            var auditTrailRecord = new ProjectRecordAuditTrail
            {
                DateTimeStamp = DateTime.UtcNow,
                ProjectRecordId = projectRecord.Id,
                User = userEmail,
                Description = p.Metadata.Name switch
                {
                    nameof(ProjectRecord.Status) => GenerateStatusChangeDescription(p.CurrentValue!.ToString()!),
                    _ => $"{p.Metadata.Name} changed from '{p.OriginalValue}' to '{p.CurrentValue}'",
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
            ProjectRecordStatus.Active => "Project record created",
            _ => "",
        };
    }
}