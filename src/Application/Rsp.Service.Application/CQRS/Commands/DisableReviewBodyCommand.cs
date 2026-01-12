using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class DisableReviewBodyCommand(Guid id) : IRequest<ReviewBodyDto?>
{
    public Guid ReviewBodyId { get; set; } = id;
}