using MediatR;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetModificationsQuery(
    ModificationSearchRequest searchQuery,
    int pageNumber,
    int pageSize,
    string sortField,
    string sortDirection
) : BaseQuery, IRequest<ModificationSearchResponse>
{
    public ModificationSearchRequest SearchQuery { get; } = searchQuery;
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public string SortField { get; } = sortField;
    public string SortDirection { get; } = sortDirection;
}