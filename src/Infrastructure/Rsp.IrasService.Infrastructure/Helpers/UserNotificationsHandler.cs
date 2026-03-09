using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.Service.Application.Constants;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.Helpers;

[ExcludeFromCodeCoverage]
public class UserNotificationsHandler
{
    private IrasContext? context;

    public List<UserNotification> CreateUserNotifications(EntityEntry entry)
    {
        if (entry.Context is not IrasContext irasContext)
        {
            return [];
        }

        context = irasContext;

        var status = GetStatusChangeInfo(entry);

        if (status.Original == status.Current)
        {
            return [];
        }

        return entry.Entity switch
        {
            ProjectModification pm => GenerateUserNotifications(pm, status),
            ProjectRecord pr => GenerateUserNotifications(pr, status),
            _ => []
        };
    }

    private List<UserNotification> GenerateUserNotifications
    (
        ProjectModification pm,
        (string? Original, string? Current) statusChangeInfo
    )
    {
        List<UserNotification> notifications = [];

        var pr = GetProjectRecord(pm.ProjectRecordId);
        var (sponsorOrgRecipients, rtsId) = GetSponsorOrgRecipients(pm.ProjectRecordId);
        var applicantId = Guid.Parse(pm.CreatedBy);

        if (statusChangeInfo.Current == ModificationStatus.WithSponsor)
        {
            foreach (var userId in sponsorOrgRecipients)
            {
                notifications.Add(new UserNotification
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Text = $"Modification {pm.ModificationIdentifier} needs your authorisation",
                    TargetUrl = "/sponsorworkspace/checkandauthorise?" +
                        $"projectRecordId={pm.ProjectRecordId}" +
                        $"irasId={pr.IrasId}" +
                        $"shortTitle={pr.ShortProjectTitle}" +
                        $"projectModificationId={pm.Id}" +
                        $"sponsorOrganisationUserId={userId}" +
                        $"rtsId={rtsId}",
                    Type = UserNotificationTypes.Action,
                    DateTimeCreated = DateTime.UtcNow,
                    RelatedEntityId = pm.Id.ToString(),
                    RelatedEntityType = nameof(ProjectModification)
                });
            }
        }

        if (statusChangeInfo.Current is ModificationStatus.NotAuthorised or ModificationStatus.RequestRevisions) // request revisions status?
        {
            notifications.Add(new UserNotification
            {
                Id = Guid.NewGuid(),
                UserId = applicantId,
                Text = $"Modification {pm.ModificationIdentifier} was not authorised",
                Type = UserNotificationTypes.Information,
                DateTimeCreated = DateTime.UtcNow,
                RelatedEntityId = pm.Id.ToString(),
                RelatedEntityType = nameof(ProjectModification)
            });
        }

        if (statusChangeInfo.Current == ModificationStatus.WithReviewBody)
        {
            notifications.Add(new UserNotification
            {
                Id = Guid.NewGuid(),
                UserId = applicantId,
                Text = $"Modification {pm.ModificationIdentifier} was authorised and sent to the review body",
                Type = UserNotificationTypes.Information,
                DateTimeCreated = DateTime.UtcNow,
                RelatedEntityId = pm.Id.ToString(),
                RelatedEntityType = nameof(ProjectModification)
            });
        }

        if (statusChangeInfo.Current is ModificationStatus.Approved or ModificationStatus.NotApproved)
        {
            foreach (var userId in sponsorOrgRecipients)
            {
                notifications.Add(new UserNotification
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Text = $"Review outcome is available for {pm.ModificationIdentifier}",
                    Type = UserNotificationTypes.Information,
                    DateTimeCreated = DateTime.UtcNow,
                    RelatedEntityId = pm.Id.ToString(),
                    RelatedEntityType = nameof(ProjectModification)
                });
            }

            if (!sponsorOrgRecipients.Contains(applicantId))
            {
                notifications.Add(new UserNotification
                {
                    Id = Guid.NewGuid(),
                    UserId = applicantId,
                    Text = $"Review outcome is available for {pm.ModificationIdentifier}",
                    Type = UserNotificationTypes.Information,
                    DateTimeCreated = DateTime.UtcNow,
                    RelatedEntityId = pm.Id.ToString(),
                    RelatedEntityType = nameof(ProjectModification)
                });
            }
        }

        if (statusChangeInfo.Current == ModificationStatus.RequestRevisions)
        {
            notifications.Add(new UserNotification
            {
                Id = Guid.NewGuid(),
                UserId = applicantId,
                Text = $"You need to respond to sponsor queries for modification {pm.ModificationIdentifier}",
                Type = UserNotificationTypes.Action,
                TargetUrl = "/modifications/requestforrevision?" +
                    $"projectRecordId={pm.ProjectRecordId}" +
                    $"irasId={pr.IrasId}" +
                    $"shortTitle={pr.ShortProjectTitle}" +
                    $"projectModificationId={pm.Id}",
                DateTimeCreated = DateTime.UtcNow,
                RelatedEntityId = pm.Id.ToString(),
                RelatedEntityType = nameof(ProjectModification)
            });
        }

        return notifications;
    }

    private List<UserNotification> GenerateUserNotifications
    (
        ProjectRecord pr,
        (string? Original, string? Current) statusInfo
    )
    {
        List<UserNotification> notifications = [];
        var (sponsorOrgRecipients, rtsId) = GetSponsorOrgRecipients(pr.Id);
        var applicantId = Guid.Parse(pr.CreatedBy);

        if (statusInfo == (ProjectRecordStatus.InDraft, ProjectRecordStatus.Active))
        {
            foreach (var userId in sponsorOrgRecipients)
            {
                notifications.Add(new UserNotification
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Text = $"Project {pr.IrasId} is now active",
                    Type = UserNotificationTypes.Information,
                    DateTimeCreated = DateTime.UtcNow,
                    RelatedEntityId = pr.Id,
                    RelatedEntityType = nameof(ProjectRecord)
                });
            }

            if (!sponsorOrgRecipients.Contains(applicantId))
            {
                notifications.Add(new UserNotification
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse(pr.CreatedBy),
                    Text = $"Project {pr.IrasId} is now active",
                    Type = UserNotificationTypes.Information,
                    DateTimeCreated = DateTime.UtcNow,
                    RelatedEntityId = pr.Id,
                    RelatedEntityType = nameof(ProjectRecord)
                });
            }
        }

        if (statusInfo == (ProjectRecordStatus.Active, ProjectRecordStatus.PendingClosure))
        {
            foreach (var userId in sponsorOrgRecipients)
            {
                notifications.Add(new UserNotification
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Text = $"Project {pr.IrasId} is pending closure and needs your authorisation",
                    Type = UserNotificationTypes.Action,
                    TargetUrl = "/sponsorworkspace/checkandauthoriseprojectclosure?" +
                        $"projectRecordId={pr.Id}" +
                        $"sponsorOrganisationUserId={userId}" +
                        $"rtsId={rtsId}",
                    DateTimeCreated = DateTime.UtcNow,
                    RelatedEntityId = pr.Id,
                    RelatedEntityType = nameof(ProjectRecord)
                });
            }

            if (!sponsorOrgRecipients.Contains(applicantId))
            {
                notifications.Add(new UserNotification
                {
                    Id = Guid.NewGuid(),
                    UserId = applicantId,
                    Text = $"Project {pr.IrasId} is pending closure",
                    Type = UserNotificationTypes.Information,
                    DateTimeCreated = DateTime.UtcNow,
                    RelatedEntityId = pr.Id,
                    RelatedEntityType = nameof(ProjectRecord)
                });
            }
        }

        if (statusInfo == (ProjectRecordStatus.PendingClosure, ProjectRecordStatus.Closed))
        {
            if (!sponsorOrgRecipients.Contains(applicantId))
            {
                notifications.Add(new UserNotification
                {
                    Id = Guid.NewGuid(),
                    UserId = applicantId,
                    Text = $"Project {pr.IrasId} is closed",
                    Type = UserNotificationTypes.Information,
                    DateTimeCreated = DateTime.UtcNow,
                    RelatedEntityId = pr.Id,
                    RelatedEntityType = nameof(ProjectRecord)
                });
            }

            foreach (var userId in sponsorOrgRecipients)
            {
                notifications.Add(new UserNotification
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Text = $"Project {pr.IrasId} is closed",
                    Type = UserNotificationTypes.Information,
                    DateTimeCreated = DateTime.UtcNow,
                    RelatedEntityId = pr.Id,
                    RelatedEntityType = nameof(ProjectRecord)
                });
            }
        }

        if (statusInfo == (ProjectRecordStatus.PendingClosure, ProjectRecordStatus.Active))
        {
            foreach (var userId in sponsorOrgRecipients)
            {
                notifications.Add(new UserNotification
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Text = $"Project {pr.IrasId} closure request was not authorised",
                    Type = UserNotificationTypes.Information,
                    DateTimeCreated = DateTime.UtcNow,
                    RelatedEntityId = pr.Id,
                    RelatedEntityType = nameof(ProjectRecord)
                });
            }

            if (!sponsorOrgRecipients.Contains(applicantId))
            {
                notifications.Add(new UserNotification
                {
                    Id = Guid.NewGuid(),
                    UserId = applicantId,
                    Text = $"Project {pr.IrasId} closure request was not authorised",
                    Type = UserNotificationTypes.Information,
                    DateTimeCreated = DateTime.UtcNow,
                    RelatedEntityId = pr.Id,
                    RelatedEntityType = nameof(ProjectRecord)
                });
            }
        }

        return notifications;
    }

    private static (string? Original, string? Current) GetStatusChangeInfo(EntityEntry entry)
    {
        var statusProperty = entry.Entity switch
        {
            ProjectModification => nameof(ProjectModification.Status),
            ProjectRecord => nameof(ProjectRecord.Status),
            _ => null
        };

        return entry.Properties.FirstOrDefault(p => p.Metadata.Name == statusProperty) is PropertyEntry statusProp
            ? (statusProp.OriginalValue as string, statusProp.CurrentValue as string)
            : (null, null);
    }

    private (List<Guid> userIds, string rtsId) GetSponsorOrgRecipients(string projectRecordId)
    {
        // TODO: Ensure to get the latest response for sponsor organisation question in
        // case there are multiple answers (e.g. due to modifications)
        var rtsId = context!.ProjectRecordAnswers
            .Where(pra => pra.ProjectRecordId == projectRecordId)
            .Where(pra => pra.QuestionId == ProjectRecordConstants.SponsorOrganisation)
            .First().Response!;

        var userIds = context!.SponsorOrganisationsUsers
            .Where(sou => sou.RtsId == rtsId)
            .Where(sou => sou.IsActive)
            .Where(sou => sou.IsAuthoriser)
            .Select(sou => sou.UserId)
            .ToList();

        return (userIds, rtsId);
    }

    private ProjectRecord GetProjectRecord(string projectRecordId)
    {
        return context!.ProjectRecords.First(pr => pr.Id == projectRecordId);
    }
}