namespace Rsp.Service.Application.DTOS.Requests;

/// <summary>
/// Request DTO containing answers for a project modification.
/// </summary>
public record ModificationAnswersRequest
{
    /// <summary>
    /// Gets or sets the project modification identifier.
    /// </summary>
    public Guid ProjectModificationId { get; set; }

    /// <summary>
    /// Gets or sets the project record identifier.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the project user identifier.
    /// </summary>
    public string UserId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the list of respondent answers for the modification.
    /// </summary>
    public List<RespondentAnswerDto> ModificationAnswers { get; set; } = [];
}