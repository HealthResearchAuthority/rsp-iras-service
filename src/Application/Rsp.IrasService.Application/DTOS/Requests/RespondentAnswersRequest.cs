﻿namespace Rsp.IrasService.Application.DTOS.Requests;

public record RespondentAnswersRequest
{
    /// <summary>
    /// IRAS Project Id
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Application Id
    /// </summary>
    public string ProjectApplicationId { get; set; } = null!;

    /// <summary>
    /// Respondent Answers
    /// </summary>
    public List<RespondentAnswerDto> RespondentAnswers { get; set; } = [];
}