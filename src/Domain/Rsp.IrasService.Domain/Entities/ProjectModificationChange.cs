namespace Rsp.IrasService.Domain.Entities;

/// <summary>
/// Represents a change made to a project modification record.
/// </summary>
public class ProjectModificationChange
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
    /// Gets or sets the area of the project that was changed.
    /// </summary>
    public int AreaOfChange { get; set; }

    /// <summary>
    /// Gets or sets the specific detail or sub-area of the change.
    /// </summary>
    public string SpecificAreaOfChange { get; set; } = null!;

    /// <summary>
    /// Gets or sets the current status of this change.
    /// </summary>
    public string Status { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date and time when this change was created.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the date and time when this change was last updated.
    /// </summary>
    public DateTime UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the user who created this change.
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who last updated this change.
    /// </summary>
    public string UpdatedBy { get; set; } = null!;
}