using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

/// <summary>
/// Handler for retrieving modification answers for a project modification change.
/// </summary>
public class GetModificationChangeAnswersHandler(IRespondentService respondentService) : IRequestHandler<GetModificationChangeAnswersQuery, IEnumerable<RespondentAnswerDto>>
{
    /// <summary>
    /// Handles retrieval of respondent answers for a given modification change.
    /// </summary>
    /// <param name="request">The query containing the identifiers for the modification change and project.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of respondent answer DTOs.</returns>
    public async Task<IEnumerable<RespondentAnswerDto>> Handle(GetModificationChangeAnswersQuery request, CancellationToken cancellationToken)
    {
        return request.CategoryId == null ?
            await respondentService.GetModificationChangeResponses(request.ProjectModificationChangeId, request.ProjectRecordId) :
            await respondentService.GetModificationChangeResponses(request.ProjectModificationChangeId, request.ProjectRecordId, request.CategoryId);
    }
}