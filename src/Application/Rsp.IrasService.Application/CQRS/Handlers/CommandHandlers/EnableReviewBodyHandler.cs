using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class EnableReviewBodyHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<EnableReviewBodyCommand, ReviewBodyDto?>
{
    public async Task<ReviewBodyDto?> Handle(EnableReviewBodyCommand request, CancellationToken cancellationToken)
    {
        return await reviewBodyService.EnableReviewBody(request.ReviewBodyId);
    }
}