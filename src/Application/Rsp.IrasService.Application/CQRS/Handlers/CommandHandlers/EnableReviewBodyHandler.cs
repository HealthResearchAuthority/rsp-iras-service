using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class EnableReviewBodyHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<EnableReviewBodyCommand, ReviewBodyDto?>
{
    public async Task<ReviewBodyDto?> Handle(EnableReviewBodyCommand request, CancellationToken cancellationToken)
    {
        return await reviewBodyService.EnableReviewBody(request.ReviewBodyId);
    }
}