using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationAnswersHandler(IRespondentService respondentService) : IRequestHandler<GetModificationAnswersQuery, IEnumerable<RespondentAnswerDto>>
{
    public async Task<IEnumerable<RespondentAnswerDto>> Handle(GetModificationAnswersQuery request, CancellationToken cancellationToken)
    {
        return request.CategoryId == null ?
            await respondentService.GetResponses(request.ProjectModificationChangeId, request.ProjectRecordId) :
            await respondentService.GetResponses(request.ProjectModificationChangeId, request.ProjectRecordId, request.CategoryId);
    }
}