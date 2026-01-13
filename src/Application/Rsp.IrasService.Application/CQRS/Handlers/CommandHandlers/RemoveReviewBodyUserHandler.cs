using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class RemoveReviewBodyUserHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<RemoveReviewBodyUserCommand, ReviewBodyUserDto?>
{
    public async Task<ReviewBodyUserDto?> Handle(RemoveReviewBodyUserCommand request, CancellationToken cancellationToken)
    {
        return await reviewBodyService.RemoveUserFromReviewBody(request.ReviewBodyId, request.UserId);
    }
}