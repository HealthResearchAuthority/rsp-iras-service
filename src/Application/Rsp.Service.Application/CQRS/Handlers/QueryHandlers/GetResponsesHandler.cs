using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetResponsesHandler(IRespondentService respondentService) : IRequestHandler<GetResponsesQuery, IEnumerable<RespondentAnswerDto>>
{
    public async Task<IEnumerable<RespondentAnswerDto>> Handle(GetResponsesQuery request, CancellationToken cancellationToken)
    {
        return request.CategoryId == null ?
            await respondentService.GetResponses(request.ApplicationId) :
            await respondentService.GetResponses(request.ApplicationId, request.CategoryId);
    }
}