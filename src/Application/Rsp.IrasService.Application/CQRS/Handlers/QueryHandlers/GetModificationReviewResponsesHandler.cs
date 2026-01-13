using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationReviewResponsesHandler(IProjectModificationService projectModificationService) : IRequestHandler<GetModificationReviewResponsesQuery, ModificationReviewResponse>
{
    public async Task<ModificationReviewResponse> Handle(GetModificationReviewResponsesQuery request, CancellationToken cancellationToken)
    {
        return await projectModificationService.GetModificationReviewResponses(request.ProjectRecordId, request.ProjectModificationId);
    }
}