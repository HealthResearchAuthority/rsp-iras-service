using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class GetReviewBodyUserByIdsCommand(List<Guid> ids) : IRequest<List<ReviewBodyUserDto>>
{
    public List<Guid> Ids { get; set; } = ids;
}