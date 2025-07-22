using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetReviewBodiesQuery(
    int pageNumber,
    int pageSize,
    string sortField,
    string sortDirection,
    ReviewBodySearchRequest searchQuery) : IRequest<AllReviewBodiesResponse>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public string SortField { get; } = sortField;
    public string SortDirection { get; } = sortDirection;
    public ReviewBodySearchRequest SearchQuery { get; } = searchQuery;
}