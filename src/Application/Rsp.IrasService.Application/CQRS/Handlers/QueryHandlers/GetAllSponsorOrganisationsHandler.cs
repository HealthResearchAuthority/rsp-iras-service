using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

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