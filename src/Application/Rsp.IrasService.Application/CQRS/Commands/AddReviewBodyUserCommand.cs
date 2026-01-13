using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class AddReviewBodyUserCommand(ReviewBodyUserDto reviewBodyUserRequest) : IRequest<ReviewBodyUserDto>
{
    public ReviewBodyUserDto ReviewBodyUserRequest { get; set; } = reviewBodyUserRequest;
}