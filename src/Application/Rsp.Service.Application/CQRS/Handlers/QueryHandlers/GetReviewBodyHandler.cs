using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetReviewBodyHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<GetReviewBodyQuery, ReviewBodyDto>
{
    public async Task<ReviewBodyDto> Handle(GetReviewBodyQuery request,
        CancellationToken cancellationToken)
    {
        return await reviewBodyService.GetReviewBody(request.Id);
    }
}