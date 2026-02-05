using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.Service.Application.Constants;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.Helpers;

[ExcludeFromCodeCoverage]
public class ProjectModificationChangeAuditTrailHandler : IAuditTrailHandler<ProjectRecordAuditTrail>
{
    /// <summary>
    /// Maps the areas of change to their specific areas of change.
    /// </summary>
    private static readonly Dictionary<string, List<string>> AreasOfChange = new()
    {
        // Administrative details for the project / project identification, contact details
        { Application.Constants.AreasOfChange.AdministrativeDetails, [SpecificAreasOfChange.ProjectIdentification, SpecificAreasOfChange.ContactDetails] },
        // Project design / Change of planned end date
        { Application.Constants.AreasOfChange.ProjectDesign, [SpecificAreasOfChange.PlannedEndDate] },
        // Project personnel / Change of Chief Investigator
        { Application.Constants.AreasOfChange.ProjectPersonal, [SpecificAreasOfChange.ChiefInvestigator] }
    };

    /// <summary>
    /// Determines whether this handler can process the specified entity.
    /// </summary>
    /// <param name="entity">The entity to check.</param>
    /// <returns>True if the entity is a ProjectModificationChange; otherwise, false.</returns>
    public bool CanHandle(object entity) => entity is ProjectModificationChange;

    /// <summary>
    /// Generates audit trail records for project modification changes that have been approved. Only
    /// processes changes for specific areas of change that are tracked.
    /// </summary>
    /// <param name="entry">The entity entry containing the change tracking information.</param>
    /// <param name="userEmail">The email of the user making the change.</param>
    /// <returns>A collection of audit trail records for the changes.</returns>
    public IEnumerable<ProjectRecordAuditTrail> GenerateAuditTrails(EntityEntry entry, string userEmail)
    {
        if (entry.Entity is not ProjectModificationChange projectModificationChange)
        {
            return [];
        }

        // Only generate audit trails for the areas and specific areas of change we care about
        if (!AreasOfChange.TryGetValue(projectModificationChange.AreaOfChange, out List<string>? specificAreasOfChanges))
        {
            return [];
        }

        if (!specificAreasOfChanges.Contains(projectModificationChange.SpecificAreaOfChange))
        {
            return [];
        }

        // Only handle modified state for approved changes
        return entry.State switch
        {
            EntityState.Modified => HandleModifiedState(entry, projectModificationChange, userEmail),
            _ => []
        };
    }

    /// <summary>
    /// Handles the modified state of a project modification change by generating audit trails for
    /// each approved change answer.
    /// </summary>
    /// <param name="entry">The entity entry containing the change tracking information.</param>
    /// <param name="projectModificationChange">The project modification change entity.</param>
    /// <param name="userEmail">The email of the user making the change.</param>
    /// <returns>A list of audit trail records for the approved changes.</returns>
    private static List<ProjectRecordAuditTrail> HandleModifiedState(EntityEntry entry, ProjectModificationChange projectModificationChange, string userEmail)
    {
        var context = entry.Context as IrasContext;

        var result = new List<ProjectRecordAuditTrail>();

        // Get the status property to check if it has changed to Approved
        var status = entry
            .Properties
            .SingleOrDefault
            (
                p =>
                    p.Metadata.Name.Equals(nameof(projectModificationChange.Status)) &&
                    !Equals(p.OriginalValue, p.CurrentValue)
            );

        if (status?.CurrentValue is not ModificationStatus.Approved)
        {
            return []; // Skip the audit trail if the status is not changed to Approved
        }

        // Get all the modification change answers related to this modification change
        var modificationChangeAnswers = context!.ProjectModificationChangeAnswers
            .AsNoTracking()
            .AsSplitQuery()
            .Where(mca => mca.ProjectModificationChangeId == projectModificationChange.Id)
            .Include(mca => mca.ProjectRecord)
            .Include(mca => mca.ProjectModificationChange)
            .OrderBy(mca => mca.ProjectModificationChange!.CreatedDate)
            .ToList();

        // Generate audit trail records for each change answer
        foreach (var changeAnswer in modificationChangeAnswers)
        {
            // Get the original answers for the project record
            var projectRecordAnswers = context.EffectiveProjectRecordAnswers
               .Where(pra => pra.ProjectRecordId == changeAnswer.ProjectRecordId)
               .ToList();

            // Get the answer for the specific question
            var projectRecordAnswer = projectRecordAnswers
                .SingleOrDefault(pra => pra.QuestionId == changeAnswer.QuestionId);

            if (projectRecordAnswer is null)
            {
                continue;
            }

            // if the answers are the same, skip, as there is no change
            if (projectRecordAnswer.Response!.Equals(changeAnswer.Response!, StringComparison.OrdinalIgnoreCase))
            {
                continue; // No change detected
            }

            var description = GenerateChangeDescription(changeAnswer, projectRecordAnswer);

            if (string.IsNullOrWhiteSpace(description))
            {
                continue;
            }

            var projectModification = context.ProjectModifications.FirstOrDefault(x =>
                x.Id == changeAnswer.ProjectModificationChange.ProjectModificationId);

            var auditTrailRecord = new ProjectRecordAuditTrail
            {
                DateTimeStamp = DateTime.UtcNow,
                ProjectRecordId = projectRecordAnswer!.ProjectRecordId,
                User = userEmail,
                Description = description,
                ModificationIdentifier = projectModification?.ModificationIdentifier
            };

            result.Add(auditTrailRecord);
        }

        return result;
    }

    /// <summary>
    /// Generates a human-readable description of the change based on the question ID.
    /// </summary>
    /// <param name="changeAnswer">The modification change answer containing the new value.</param>
    /// <param name="projectRecordAnswer">
    /// The original project record answer containing the old value.
    /// </param>
    /// <returns>A description of the change, or an empty string if the question ID is not recognized.</returns>
    private static string GenerateChangeDescription(ProjectModificationChangeAnswer changeAnswer, EffectiveProjectRecordAnswer? projectRecordAnswer)
    {
        var changeFromToText = $"changed from '{projectRecordAnswer!.Response}' to '{changeAnswer.Response}'";

        return changeAnswer.QuestionId switch
        {
            ModificationChangeQuestionIds.ChiefInvestigatorFirstName => $"Chief Investigator first name {changeFromToText}",
            ModificationChangeQuestionIds.ChiefInvestigatorLastName => $"Chief Investigator last name {changeFromToText}",
            ModificationChangeQuestionIds.ChiefInvestigatorEmail => $"Chief Investigator email {changeFromToText}",
            ModificationChangeQuestionIds.ProjectEndDate => $"Planned project end date {changeFromToText}",
            ModificationChangeQuestionIds.ShortProjectTitle => $"Short project title {changeFromToText}",
            ModificationChangeQuestionIds.FullProjectTitle => $"Full project title {changeFromToText}",
            _ => string.Empty,
        };
    }
}