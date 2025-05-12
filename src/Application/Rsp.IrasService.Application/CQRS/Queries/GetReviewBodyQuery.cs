using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetReviewBodyQuery(Guid id) : IRequest<ReviewBodyDto>
{
    public Guid Id { get; } = id;
}