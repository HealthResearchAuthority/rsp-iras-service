namespace Rsp.IrasService.Application.DTOS.Requests;

public record ModificationChangeRequest
{
    /// <summary>
    /// The public key for the application database record
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// The public key for the application database record
    /// </summary>
    public Guid ProjectModificationId { get; set; }

    /// <summary>
    /// The title of the project
    /// </summary>
    public string AreaOfChange { get; set; } = null!;

    /// <summary>
    /// The title of the project
    /// </summary>
    public string SpecificAreaOfChange { get; set; } = null!;

    /// <summary>
    /// Application Status
    /// </summary>
    public string Status { get; set; } = null!;

    /// <summary>
    /// User Id who initiated the application
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// User Id who updated the application
    /// </summary>
    public string UpdatedBy { get; set; } = null!;

    /// <summary>
    /// The date the modification was created
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// The date the modification was updated
    /// </summary>
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}