using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class AddReviewBodyUserHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<AddReviewBodyUserCommand, ReviewBodyUserDto>
{
    public async Task<ReviewBodyUserDto> Handle(AddReviewBodyUserCommand request, CancellationToken cancellationToken)
    {
        return await reviewBodyService.AddUserToReviewBody(request.ReviewBodyUserRequest);
    }
}