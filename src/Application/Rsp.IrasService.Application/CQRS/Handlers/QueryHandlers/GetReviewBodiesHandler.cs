using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetReviewBodiesHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<GetReviewBodiesQuery, AllReviewBodiesResponse>
{
    public async Task<AllReviewBodiesResponse> Handle(GetReviewBodiesQuery request,
        CancellationToken cancellationToken)
    {
        return await reviewBodyService.GetReviewBodies(request.PageNumber, request.PageSize, request.SearchQuery);
    }
}