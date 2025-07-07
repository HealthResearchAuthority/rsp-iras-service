using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

/// <summary>
/// Handler for retrieving modification answers for a project modification change.
/// </summary>
public class GetModificationAnswersHandler(IRespondentService respondentService) : IRequestHandler<GetModificationAnswersQuery, IEnumerable<RespondentAnswerDto>>
{
    /// <summary>
    /// Handles retrieval of respondent answers for a given modification change.
    /// </summary>
    /// <param name="request">The query containing the identifiers for the modification change and project.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of respondent answer DTOs.</returns>
    public async Task<IEnumerable<RespondentAnswerDto>> Handle(GetModificationAnswersQuery request, CancellationToken cancellationToken)
    {
        return request.CategoryId == null ?
            await respondentService.GetResponses(request.ProjectModificationChangeId, request.ProjectRecordId) :
            await respondentService.GetResponses(request.ProjectModificationChangeId, request.ProjectRecordId, request.CategoryId);
    }
}