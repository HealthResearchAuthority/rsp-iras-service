using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetAllSponsorOrganisationsHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<GetSponsorOrganisationsQuery, AllSponsorOrganisationsResponse>
{
    public async Task<AllSponsorOrganisationsResponse> Handle(GetSponsorOrganisationsQuery request,
        CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.GetSponsorOrganisations(request.PageNumber, request.PageSize,
            request.SortField, request.SortDirection, request.SearchQuery);
    }
}