using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class GetReviewBodyUserByIdsCommand(List<Guid> ids) : IRequest<List<ReviewBodyUserDto>>
{
    public List<Guid> Ids { get; set; } = ids;
}