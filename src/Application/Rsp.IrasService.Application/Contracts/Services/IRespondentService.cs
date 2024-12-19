using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.Logging.Interceptors;

namespace Rsp.IrasService.Application.Contracts.Services;

/// <summary>
/// Interface to create/read/update the application records in the database. Marked as IInterceptable to enable
/// the start/end logging for all methods.
/// </summary>
public interface IRespondentService : IInterceptable
{
    Task<IEnumerable<RespondentAnswerDto>> GetResponses(string applicationId);

    Task<IEnumerable<RespondentAnswerDto>> GetResponses(string applicationId, string categoryId);

    Task SaveResponses(RespondentAnswersRequest respondentAnswersRequest);
}