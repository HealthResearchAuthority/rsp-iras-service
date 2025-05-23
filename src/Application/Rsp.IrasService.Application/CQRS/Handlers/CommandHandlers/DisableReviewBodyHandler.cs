using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class DisableReviewBodyHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<DisableReviewBodyCommand, ReviewBodyDto?>
{
    public async Task<ReviewBodyDto?> Handle(DisableReviewBodyCommand request, CancellationToken cancellationToken)
    {
        return await reviewBodyService.DisableReviewBody(request.ReviewBodyId);
    }
}