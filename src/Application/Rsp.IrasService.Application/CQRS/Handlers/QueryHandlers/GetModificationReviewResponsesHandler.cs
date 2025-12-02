using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationReviewResponsesHandler(IProjectModificationService projectModificationService) : IRequestHandler<GetModificationReviewResponsesQuery, ModificationReviewResponse>
{
    public async Task<ModificationReviewResponse> Handle(GetModificationReviewResponsesQuery request, CancellationToken cancellationToken)
    {
        return await projectModificationService.GetModificationReviewResponses(request.ProjectRecordId, request.ProjectModificationId);
    }
}