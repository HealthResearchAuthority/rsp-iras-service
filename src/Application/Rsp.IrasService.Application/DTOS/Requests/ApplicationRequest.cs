namespace Rsp.Service.Application.DTOS.Requests;

public record ApplicationRequest
{
    /// <summary>
    /// The public key for the application database record
    /// </summary>
    public string Id { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");

    /// <summary>
    /// The title of the project
    /// </summary>
    public string ShortProjectTitle { get; set; } = null!;

    /// <summary>
    /// Description of the application
    /// </summary>
    public string FullProjectTitle { get; set; } = null!;

    /// <summary>
    /// The start date of the project
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Application Status
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// User Id who initiated the application
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// User Id who updated the application
    /// </summary>
    public string UpdatedBy { get; set; } = null!;

    /// <summary>
    /// Id of the user creating the application
    /// </summary>
    public string UserId { get; set; } = null!;

    /// <summary>
    /// IRAS ID of the application
    /// </summary>
    public int? IrasId { get; set; }
}