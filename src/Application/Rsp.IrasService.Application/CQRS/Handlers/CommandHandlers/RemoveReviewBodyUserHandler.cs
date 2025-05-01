using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class RemoveReviewBodyUserHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<RemoveReviewBodyUserCommand, ReviewBodyUserDto?>
{
    public async Task<ReviewBodyUserDto?> Handle(RemoveReviewBodyUserCommand request, CancellationToken cancellationToken)
    {
        return await reviewBodyService.RemoveUserFromReviewBody(request.ReviewBodyId, request.UserId);
    }
}