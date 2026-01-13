using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class EnableReviewBodyCommand(Guid id) : IRequest<ReviewBodyDto?>
{
    public Guid ReviewBodyId { get; set; } = id;
}