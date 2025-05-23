using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class AddReviewBodyUserCommand(ReviewBodyUserDto reviewBodyUserRequest) : IRequest<ReviewBodyUserDto>
{
    public ReviewBodyUserDto ReviewBodyUserRequest { get; set; } = reviewBodyUserRequest;
}