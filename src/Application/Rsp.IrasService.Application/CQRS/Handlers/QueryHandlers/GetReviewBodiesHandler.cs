using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetReviewBodiesHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<GetReviewBodiesQuery, IEnumerable<ReviewBodyDto>>
{
    public async Task<IEnumerable<ReviewBodyDto>> Handle(GetReviewBodiesQuery request,
        CancellationToken cancellationToken)
    {
        return !request.Id.HasValue ?
            await reviewBodyService.GetReviewBodies() :
            await reviewBodyService.GetReviewBodies(request.Id.Value);
    }
}