using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class DisableReviewBodyHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<DisableReviewBodyCommand, ReviewBodyDto?>
{
    public async Task<ReviewBodyDto?> Handle(DisableReviewBodyCommand request, CancellationToken cancellationToken)
    {
        return await reviewBodyService.DisableReviewBody(request.ReviewBodyId);
    }
}