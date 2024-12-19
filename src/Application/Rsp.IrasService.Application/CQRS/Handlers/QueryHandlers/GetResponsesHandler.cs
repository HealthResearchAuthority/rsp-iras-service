using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetResponsesHandler(IRespondentService respondentService) : IRequestHandler<GetResponsesQuery, IEnumerable<RespondentAnswerDto>>
{
    public async Task<IEnumerable<RespondentAnswerDto>> Handle(GetResponsesQuery request, CancellationToken cancellationToken)
    {
        return request.CategoryId == null ?
            await respondentService.GetResponses(request.ApplicationId) :
            await respondentService.GetResponses(request.ApplicationId, request.CategoryId);
    }
}