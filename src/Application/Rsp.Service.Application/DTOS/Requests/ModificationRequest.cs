namespace Rsp.Service.Application.DTOS.Requests;

/// <summary>
/// Request DTO representing a project modification.
/// </summary>
public record ModificationRequest
{
    /// <summary>
    /// Gets or sets the unique identifier for the modification.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the project record identifier.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the modification identifier.
    /// </summary>
    public string ModificationIdentifier { get; set; } = null!;

    /// <summary>
    /// Gets or sets the status of the modification.
    /// </summary>
    public string Status { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user ID who created the modification.
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user ID who last updated the modification.
    /// </summary>
    public string UpdatedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date the modification was created.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the date the modification was last updated.
    /// </summary>
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}