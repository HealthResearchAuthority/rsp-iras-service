using MediatR;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetPaginatedApplicationsWithRespondentQuery : BaseQuery, IRequest<PaginatedResponse<ApplicationResponse>>
{
    public string RespondentId { get; set; } = null!;
    public ApplicationSearchRequest SearchQuery { get; set; } = null!;
    public int PageIndex { get; set; } = 1;
    public int? PageSize { get; set; } = null;
    public string? SortField { get; set; } = null;
    public string? SortDirection { get; set; } = null;
}