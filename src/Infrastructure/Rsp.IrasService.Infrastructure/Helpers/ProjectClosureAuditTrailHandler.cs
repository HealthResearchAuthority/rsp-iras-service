using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.Service.Domain.Attributes;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.Helpers;

[ExcludeFromCodeCoverage]
public class ProjectClosureAuditTrailHandler : IAuditTrailHandler<ProjectRecordAuditTrail>
{
    public bool CanHandle(object entity) => entity is ProjectClosure;

    public IEnumerable<ProjectRecordAuditTrail> GenerateAuditTrails(EntityEntry entry, string userEmail)
    {
        if (entry.Entity is not ProjectClosure projectClosure)
        {
            return [];
        }

        return entry.State switch
        {
            EntityState.Added => [HandleAddedState(projectClosure, userEmail)],
            EntityState.Modified => HandleModifiedState(entry, projectClosure, userEmail),
            _ => []
        };
    }

    private static ProjectRecordAuditTrail HandleAddedState(ProjectClosure projectClosure, string userEmail)
    {
        return new ProjectRecordAuditTrail
        {
            DateTimeStamp = DateTime.UtcNow,
            ProjectRecordId = projectClosure.ProjectRecordId,
            User = userEmail,
            Description = "Project closure is pending (sent to sponsor)"
        };
    }

    private static List<ProjectRecordAuditTrail> HandleModifiedState(EntityEntry entry, ProjectClosure projectClosure, string userEmail)
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
                ProjectRecordId = projectClosure.ProjectRecordId,
                User = userEmail,
                Description = $"Project closure {p.Metadata.Name} changed from '{p.OriginalValue}' to '{p.CurrentValue}'"
            };

            if (!string.IsNullOrEmpty(auditTrailRecord.Description))
            {
                result.Add(auditTrailRecord);
            }
        }

        return result;
    }
}