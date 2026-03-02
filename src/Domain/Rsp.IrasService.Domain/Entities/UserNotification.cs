using System.Diagnostics.CodeAnalysis;

namespace Rsp.Service.Domain.Entities;

[ExcludeFromCodeCoverage] // Adding this as we have no implementation to test yet, should be removed once we have implemented functionality
public class UserNotification
{
    /// <summary>
    /// The unique identifier for the user notification.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier for the user associated with the notification.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The message or content of the notification.
    /// </summary>
    public string Text { get; set; } = null!;

    /// <summary>
    /// An optional URL that the notification may link to for more information or action.
    /// </summary>
    public string? TargetUrl { get; set; }

    /// <summary>
    /// Type of the notification (e.g. Action or Information)
    /// </summary>
    public string Type { get; set; } = null!;

    /// <summary>
    /// The date and time when the notification was created.
    /// </summary>
    public DateTime DateTimeCreated { get; set; }

    /// <summary>
    /// The date and time when the notification was seen by the user. This is null if the notification has not been seen yet.
    /// </summary>
    public DateTime? DateTimeSeen { get; set; }

    /// <summary>
    /// Id of the entity this notification relates to (if needed) e.g. modificationId, recordId etc.
    /// </summary>
    public string? RelatedEntityId { get; set; }

    /// <summary>
    /// Type of the related entity (e.g. Modification, ProjectRecord etc.)
    /// </summary>
    public string? RelatedEntityType { get; set; }
}