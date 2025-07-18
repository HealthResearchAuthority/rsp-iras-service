using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetPaginatedApplicationsWithRespondentQuery : IRequest<PaginatedResponse<ApplicationResponse>>
{
    public string RespondentId { get; set; } = null!;
    public string? SearchQuery { get; set; } = null;
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 0;
}