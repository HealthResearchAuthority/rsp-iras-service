using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetReviewBodiesHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<GetReviewBodiesQuery, AllReviewBodiesResponse>
{
    public async Task<AllReviewBodiesResponse> Handle(GetReviewBodiesQuery request,
        CancellationToken cancellationToken)
    {
        return await reviewBodyService.GetReviewBodies(request.PageNumber, request.PageSize, request.SortField, request.SortDirection, request.SearchQuery);
    }
}