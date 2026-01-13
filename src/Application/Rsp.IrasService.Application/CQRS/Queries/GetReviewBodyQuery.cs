using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetReviewBodyQuery(Guid id) : IRequest<ReviewBodyDto>
{
    public Guid Id { get; } = id;
}