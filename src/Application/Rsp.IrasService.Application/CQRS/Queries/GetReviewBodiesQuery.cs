using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetReviewBodiesQuery(int pageNumber, int pageSize, ReviewBodySearchRequest searchQuery) : IRequest<AllReviewBodiesResponse>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public ReviewBodySearchRequest SearchQuery { get; } = searchQuery;
}