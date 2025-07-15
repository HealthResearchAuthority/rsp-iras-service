namespace Rsp.IrasService.Application.DTOS.Requests;

/// <summary>
/// Request DTO representing a change to a project modification.
/// </summary>
public record ModificationChangeRequest
{
    /// <summary>
    /// Gets or sets the unique identifier for the modification change.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the identifier of the related project modification.
    /// </summary>
    public Guid ProjectModificationId { get; set; }

    /// <summary>
    /// Gets or sets the area of change for the modification.
    /// </summary>
    public string AreaOfChange { get; set; } = null!;

    /// <summary>
    /// Gets or sets the specific area of change for the modification.
    /// </summary>
    public string SpecificAreaOfChange { get; set; } = null!;

    /// <summary>
    /// Gets or sets the status of the modification change.
    /// </summary>
    public string Status { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user ID who created the modification change.
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user ID who last updated the modification change.
    /// </summary>
    public string UpdatedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date the modification change was created.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the date the modification change was last updated.
    /// </summary>
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}