using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class RemoveReviewBodyUserCommand(Guid reviewBodyId, Guid userId) : IRequest<ReviewBodyUserDto?>
{
    public Guid ReviewBodyId { get; set; } = reviewBodyId;
    public Guid UserId { get; set; } = userId;
}