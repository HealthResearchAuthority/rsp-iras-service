namespace Rsp.IrasService.Application.DTOS.Requests;

/// <summary>
/// Request DTO representing a project modification.
/// </summary>
public record UpdateModificationRequest : ModificationRequest
{
    /// <summary>
    /// Gets or sets the sequential number of the modification.
    /// </summary>
    public int ModificationNumber { get; set; }

    /// <summary>
    /// Overall ranking type of the modification
    /// </summary>
    public string? ModificationType { get; set; }

    /// <summary>
    /// Overall ranking category of the modification
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Overall ranking review type of the modification
    /// </summary>
    public string? ReviewType { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the reviewer assigned to this modification, if any.
    /// </summary>
    public string? ReviewerId { get; set; }

    /// <summary>
    /// Gets or sets the email of the reviewer assigned to this modification, if any.
    /// </summary>
    public string? ReviewerEmail { get; set; }

    /// <summary>
    /// Gets or sets the name of the reviewer assigned to this modification, if any.
    /// </summary>
    public string? ReviewerName { get; set; }

    /// <summary>
    /// Gets or sets the submission date.
    /// This date is populated when a researcher clicks send to sponsor from the Reveiw all changes page, the actual status is With Sponsor
    /// </summary>
    public DateTime? SentToSponsorDate { get; set; }

    public DateTime? SentToRegulatorDate { get; set; }

    public List<UpdateModificationChangeRequest> ProjectModificationChanges { get; set; } = [];
}