namespace Rsp.IrasService.Application.DTOS.Requests;

/// <summary>
/// Request object containing respondent's answers for a specific project record.
/// </summary>
public record RespondentAnswersRequest
{
    /// <summary>
    /// Unique identifier for the respondent.
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Identifier for the associated project record.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// List of answers provided by the respondent.
    /// </summary>
    public List<RespondentAnswerDto> RespondentAnswers { get; set; } = [];
}