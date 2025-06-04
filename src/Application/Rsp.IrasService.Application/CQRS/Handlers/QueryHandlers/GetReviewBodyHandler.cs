using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetReviewBodyHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<GetReviewBodyQuery, ReviewBodyDto>
{
    public async Task<ReviewBodyDto> Handle(GetReviewBodyQuery request,
        CancellationToken cancellationToken)
    {
        return await reviewBodyService.GetReviewBody(request.Id);
    }
}