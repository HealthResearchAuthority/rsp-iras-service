using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.Service.Application.Constants;
using Rsp.Service.Domain.Attributes;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.Helpers;

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
            Description = "Modification created"
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
                Attribute.IsDefined(p.Metadata.PropertyInfo!, typeof(AuditableAttribute)) &&
                !Equals(p.OriginalValue, p.CurrentValue));

        var result = new List<ProjectModificationAuditTrail>();

        foreach (var p in modifiedAuditableProps)
        {
            var logs = p.Metadata.Name switch
            {
                nameof(projectModification.Status) =>
                    GenerateStatusChangeDescription(p.CurrentValue!.ToString()!, projectModification.ReviewType),
                nameof(projectModification.ReviewerEmail) =>
                    GenerateReviewerChangeDescription(p.CurrentValue!.ToString()!, p.OriginalValue?.ToString()),
                nameof(projectModification.ProvisionalReviewOutcome) =>
                    GenerateReviewOutcomeChangeDescription(p.CurrentValue!.ToString()!, p.OriginalValue?.ToString()),
                nameof(projectModification.ReasonNotApproved) =>
                    GenerateReasonNotApprovedChangeDescription(p.CurrentValue?.ToString(), p.OriginalValue?.ToString()),
                nameof(projectModification.ReviewerComments) =>
                    [(p.OriginalValue?.ToString() == null ? "Comment added" : "Comment changed", true, false)],
                _ => [("", true, false)]
            };

            foreach (var (Description, IsBackstageOnly, ShowUserEmailToFrontstage) in logs)
            {
                var auditTrailRecord = new ProjectModificationAuditTrail
                {
                    DateTimeStamp = DateTime.UtcNow,
                    ProjectModificationId = projectModification.Id,
                    User = userEmail,
                    Description = Description,
                    IsBackstageOnly = IsBackstageOnly,
                    ShowUserEmailToFrontstage = ShowUserEmailToFrontstage
                };

                if (!string.IsNullOrEmpty(auditTrailRecord.Description))
                {
                    result.Add(auditTrailRecord);
                }
            }
        }

        return result;
    }

    private static List<(string Description, bool IsBackstageOnly, bool ShowUserEmailToFrontstage)> GenerateStatusChangeDescription(string newStatus, string? reviewType)
    {
        return (newStatus, reviewType) switch
        {
            (ModificationStatus.WithSponsor, _) =>
                [("Modification submitted", true, false),
                ("Modification sent to sponsor", false, true)],

            (ModificationStatus.WithReviewBody, _) =>
                [("Modification authorised by sponsor", false, true),
                ("Modification submitted to review body", false, true)],

            (ModificationStatus.Approved, ModificationStatus.ReviewType.NoReviewRequired) =>
                [("Modification authorised by sponsor", false, true),
                ("Modification approved", false, false)],

            (ModificationStatus.Approved, ModificationStatus.ReviewType.ReviewRequired) =>
                [("Modification approved by review body", false, false),
                ("Review outcome sent to applicant", true, false)],

            (ModificationStatus.NotApproved, _) =>
                [("Modification not approved by review body", false, false),
                ("Review outcome sent to applicant", true, false)],

            (ModificationStatus.NotAuthorised, _) =>
                [("Modification not authorised by sponsor", false, true)],

            _ => [("", true, false)],
        };
    }

    private static List<(string Description, bool IsBackstageOnly, bool ShowUserEmailToFrontstage)> GenerateReviewerChangeDescription(string newReviewerEmail, string? oldReviewerEmail)
    {
        return
        [
            (
                oldReviewerEmail == null ?
                    $"Modification assigned to '{newReviewerEmail}'" :
                    $"Modification reassigned to '{newReviewerEmail}'",
                true,
                false
            )
        ];
    }

    private static List<(string Description, bool IsBackstageOnly, bool ShowUserEmailToFrontstage)> GenerateReviewOutcomeChangeDescription(string newOutcome, string? oldOutcome)
    {
        return
        [
            (
                oldOutcome == null ?
                    $"Outcome selected" :
                    $"Outcome changed from {oldOutcome} to {newOutcome}",
                true,
                false
            )
        ];
    }

    private static List<(string Description, bool IsBackstageOnly, bool ShowUserEmailToFrontstage)> GenerateReasonNotApprovedChangeDescription(string? newReason, string? oldReason)
    {
        return
        [
            (
                oldReason == null ?
                    $"Reason modification not approved added" :
                    $"Reason modification not approved changed from {oldReason} to {newReason ?? "(null)"}",
                true,
                false
            )
        ];
    }
}