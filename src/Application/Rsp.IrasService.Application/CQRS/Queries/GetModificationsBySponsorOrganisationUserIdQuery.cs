using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetModificationsBySponsorOrganisationUserIdQuery
(
    Guid sponsorOrganisationUserId,
    SponsorAuthorisationsSearchRequest searchQuery,
    int pageNumber,
    int pageSize,
    string sortField,
    string sortDirection
) : IRequest<ModificationSearchResponse>
{
    public Guid SponsorOrganisationUserId { get; } = sponsorOrganisationUserId;
    public SponsorAuthorisationsSearchRequest SearchQuery { get; } = searchQuery;
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public string SortField { get; } = sortField;
    public string SortDirection { get; } = sortDirection;
}