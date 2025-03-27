using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetReviewBodiesQuery(Guid? id = null) : IRequest<IEnumerable<ReviewBodyDto>>
{
    public Guid? Id { get; } = id;
}