using MediatR;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetProjectClosuresBySponsorOrganisationUserIdWithoutPagingQuery
(
    Guid sponsorOrganisationUserId,
    ProjectClosuresSearchRequest searchQuery,
    string rtsId
) : BaseQuery, IRequest<ProjectClosuresSearchResponse>
{
    public Guid SponsorOrganisationUserId { get; } = sponsorOrganisationUserId;
    public ProjectClosuresSearchRequest SearchQuery { get; } = searchQuery;
    public string RtsId { get; } = rtsId;
}