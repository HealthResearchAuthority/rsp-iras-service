using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class GetReviewBodyUserCommand(Guid userId) : IRequest<List<ReviewBodyUserDto>>
{
    public Guid UserId { get; set; } = userId;
}