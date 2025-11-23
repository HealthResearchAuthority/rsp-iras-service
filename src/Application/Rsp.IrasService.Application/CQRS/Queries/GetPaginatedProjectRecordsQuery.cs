using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetPaginatedProjectRecordsQuery : BaseQuery, IRequest<PaginatedResponse<CompleteProjectRecordResponse>>
{
    public ProjectRecordSearchRequest SearchQuery { get; set; } = null!;
    public int PageIndex { get; set; } = 1;
    public int? PageSize { get; set; } = null;
    public string? SortField { get; set; } = null;
    public string? SortDirection { get; set; } = null;
}