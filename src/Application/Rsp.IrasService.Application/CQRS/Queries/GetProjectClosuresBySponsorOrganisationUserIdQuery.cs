using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetProjectClosuresBySponsorOrganisationUserIdQuery
(
    Guid sponsorOrganisationUserId,
    ProjectClosuresSearchRequest searchQuery,
    int pageNumber,
    int pageSize,
    string sortField,
    string sortDirection
) : BaseQuery, IRequest<ProjectClosuresSearchResponse>
{
    public Guid SponsorOrganisationUserId { get; } = sponsorOrganisationUserId;
    public ProjectClosuresSearchRequest SearchQuery { get; } = searchQuery;
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public string SortField { get; } = sortField;
    public string SortDirection { get; } = sortDirection;
}