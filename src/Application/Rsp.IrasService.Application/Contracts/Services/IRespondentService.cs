using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.Contracts.Services;

/// <summary>
/// Interface to create/read/update the application records in the database
/// </summary>
public interface IRespondentService
{
    Task<IEnumerable<RespondentAnswerDto>> GetResponses(string applicationId);

    Task<IEnumerable<RespondentAnswerDto>> GetResponses(string applicationId, string categoryId);

    Task SaveResponses(RespondentAnswersRequest respondentAnswersRequest);
}