using MediatR;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetModificationsBySponsorOrganisationUserIdQuery
(
    Guid sponsorOrganisationUserId,
    SponsorAuthorisationsModificationsSearchRequest searchQuery,
    int pageNumber,
    int pageSize,
    string sortField,
    string sortDirection,
    string rtsId
) : BaseQuery, IRequest<ModificationSearchResponse>
{
    public Guid SponsorOrganisationUserId { get; } = sponsorOrganisationUserId;
    public SponsorAuthorisationsModificationsSearchRequest SearchQuery { get; } = searchQuery;
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public string SortField { get; } = sortField;
    public string SortDirection { get; } = sortDirection;
    public string RtsId { get; } = rtsId;
}