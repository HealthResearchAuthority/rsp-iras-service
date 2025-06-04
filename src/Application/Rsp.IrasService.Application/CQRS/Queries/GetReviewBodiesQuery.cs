using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetReviewBodiesQuery(int pageNumber, int pageSize, string? searchQuery) : IRequest<AllReviewBodiesResponse>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public string? SearchQuery { get; } = searchQuery;
}