namespace Rsp.IrasService.Domain.Entities;

/// <summary>
/// Represents a modification made to a project record.
/// </summary>
public class ProjectModification
{
    /// <summary>
    /// Gets or sets the unique identifier for the project modification.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the associated project record.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the sequential number of the modification.
    /// </summary>
    public int ModificationNumber { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the modification. This is a combination of IrasId and ModificationNumber
    /// </summary>
    public string ModificationIdentifier { get; set; } = null!;

    /// <summary>
    /// Gets or sets the status of the modification.
    /// </summary>
    public string Status { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date and time when the modification was created.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the modification was last updated.
    /// </summary>
    public DateTime UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the user who created the modification.
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who last updated the modification.
    /// </summary>
    public string UpdatedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the reviewer assigned to this modification, if any.
    /// </summary>
    public string? ReviewerId { get; set; }

    // Navigation property for the changes associated with this project modification.
    public ICollection<ProjectModificationChange> ProjectModificationChanges { get; set; } = [];
    /// <summary>
    /// Gets or sets the submission date.
    /// </summary>
    public DateTime? SubmittedDate { get; set; }
}