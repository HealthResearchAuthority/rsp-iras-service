using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class GetReviewBodyUserCommand(Guid userId) : IRequest<List<ReviewBodyUserDto>>
{
    public Guid UserId { get; set; } = userId;
}