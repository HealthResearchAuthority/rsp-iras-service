using Rsp.Service.Domain.Interfaces;

namespace Rsp.Service.Domain.Entities;

/// <summary>
/// Represents a change made to a project modification record, including details about the area changed,
/// the status of the change, and audit information such as creation and update metadata.
/// </summary>
public class ProjectModificationChange : IAuditable
{
    /// <summary>
    /// Gets or sets the unique identifier for this project modification change.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the related project modification.
    /// </summary>
    public Guid ProjectModificationId { get; set; }

    /// <summary>
    /// Ranking type of the change
    /// </summary>
    public string? ModificationType { get; set; }

    /// <summary>
    /// Ranking category of the change
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Ranking review type of the change
    /// </summary>
    public string? ReviewType { get; set; }

    /// <summary>
    /// Gets or sets the area of the project that was changed
    /// </summary>
    public string AreaOfChange { get; set; } = null!;

    /// <summary>
    /// Gets or sets the specific detail or sub-area of the change
    /// </summary>
    public string SpecificAreaOfChange { get; set; } = null!;

    /// <summary>
    /// Gets or sets the current status of this change (e.g., Pending, Approved, Rejected).
    /// </summary>
    public string Status { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date and time when this change was created (in UTC).
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the date and time when this change was last updated (in UTC).
    /// </summary>
    public DateTime UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the user who created this change (username or user ID).
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who last updated this change (username or user ID).
    /// </summary>
    public string UpdatedBy { get; set; } = null!;
}