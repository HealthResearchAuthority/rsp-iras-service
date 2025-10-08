using Mapster;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Specifications;

namespace Rsp.IrasService.Services;

public class SponsorOrganisationsService(ISponsorOrganisationsRepository sponsorOrganisationsRepository)
    : ISponsorOrganisationsService
{
    public async Task<AllSponsorOrganisationsResponse> GetSponsorOrganisations(int pageNumber, int pageSize,
        string sortField, string sortDirection,
        SponsorOrganisationSearchRequest searchQuery)
    {
        var specification =
            new GetSponsorOrganisationsSpecification(pageNumber, pageSize, sortField, sortDirection, searchQuery);

        var rbResponses = await sponsorOrganisationsRepository.GetSponsorOrganisations(specification);
        var rbCount = await sponsorOrganisationsRepository.GetSponsorOrganisationCount(searchQuery);

        var response = new AllSponsorOrganisationsResponse
        {
            SponsorOrganisations = rbResponses.Select(x => x.Adapt<SponsorOrganisationDto>()),
            TotalCount = rbCount
        };

        return response;
    }
}