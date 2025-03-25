using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class EnableReviewBodyCommand(Guid id) : IRequest<ReviewBodyDto?>
{
    public Guid ReviewBodyId { get; set; } = id;
}