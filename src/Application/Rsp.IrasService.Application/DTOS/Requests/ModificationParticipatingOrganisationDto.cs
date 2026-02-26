namespace Rsp.Service.Application.DTOS.Requests;

/// <summary>
/// Request DTO representing a modification participating organisation.
/// </summary>
public class ModificationParticipatingOrganisationDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the modification.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the identifier of the related project modification change.
    /// </summary>
    public Guid ProjectModificationChangeId { get; set; }

    /// <summary>
    /// Gets or sets the organisation Id.
    /// </summary>
    public string OrganisationId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the project record identifier.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the project user identifier.
    /// </summary>
    public string UserId { get; set; } = null!;

    /// <summary>
    /// User Id of the respondent who provided the answer
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// Date and time when the answer was provided
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// User Id of the respondent who last updated the answer
    /// </summary>
    public string UpdatedBy { get; set; } = null!;

    /// <summary>
    /// Date and time when the answer was last updated
    /// </summary>
    public DateTime UpdatedDate { get; set; }
}