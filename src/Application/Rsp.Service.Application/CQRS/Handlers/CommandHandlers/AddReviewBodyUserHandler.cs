using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class AddReviewBodyUserHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<AddReviewBodyUserCommand, ReviewBodyUserDto>
{
    public async Task<ReviewBodyUserDto> Handle(AddReviewBodyUserCommand request, CancellationToken cancellationToken)
    {
        return await reviewBodyService.AddUserToReviewBody(request.ReviewBodyUserRequest);
    }
}