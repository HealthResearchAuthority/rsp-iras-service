namespace Rsp.IrasService.Application.DTOS.Requests;

public record ModificationAnswersRequest
{
    /// <summary>
    /// Project modification change Id
    /// </summary>
    public Guid ProjectModificationChangeId { get; set; }

    /// <summary>
    /// Project record Id
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// Project personnel
    /// </summary>
    public string ProjectPersonnelId { get; set; } = null!;

    /// <summary>
    /// Respondent Answers
    /// </summary>
    public List<RespondentAnswerDto> ModificationAnswers { get; set; } = [];
}