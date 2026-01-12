namespace Rsp.IrasService.Application.DTOS.Requests;

public class ProjectClosureBase
{
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the researcher which is associated with projectrecord.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the sequential number of the project closure.
    /// </summary>
    public int ProjectClosureNumber { get; set; }

    /// <summary>
    /// Unique identifier for the project closure. This will be a combination of IrasId and ProjectClosureNumber
    /// </summary>
    public string TransactionId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier actual project closure date
    /// </summary>
    public DateTime? ClosureDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the email.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for researcher for project closure.
    /// </summary>
    public DateTime? SentToSponsorDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier when sponsor authorise.
    /// </summary>
    public DateTime? DateActioned { get; set; }

    /// <summary>
    /// Gets or sets the project title
    /// </summary>
    public string ShortProjectTitle { get; set; } = null!;

    /// <summary>
    /// Gets or sets the IRAS identifier of the project
    /// </summary>
    public int? IrasId { get; set; }

    /// <summary>
    /// Gets or sets the project closure status
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets userid of the person initiated the application
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets userId of the person updated the application
    /// </summary>
    public string UpdatedBy { get; set; } = null!;
}