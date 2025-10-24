using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetSponsorOrganisationsQuery(
    int pageNumber,
    int pageSize,
    string sortField,
    string sortDirection,
    SponsorOrganisationSearchRequest searchQuery) : IRequest<AllSponsorOrganisationsResponse>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public string SortField { get; } = sortField;
    public string SortDirection { get; } = sortDirection;
    public SponsorOrganisationSearchRequest SearchQuery { get; } = searchQuery;
}