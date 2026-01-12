using MediatR;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

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