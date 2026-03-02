namespace Rsp.Service.Application.DTOS.Requests;

/// <summary>
/// Request DTO representing a project modification.
/// </summary>
public record DuplicateModificationRequest
{
    /// <summary>
    /// Gets or sets the project record identifier.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the modification identifier.
    /// </summary>
    public Guid ExistingModificationId { get; set; } 

    /// <summary>
    /// Gets or sets the date the modification was created.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the date the modification was last updated.
    /// </summary>
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}