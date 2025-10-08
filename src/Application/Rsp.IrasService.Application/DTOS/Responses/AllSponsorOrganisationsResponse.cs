using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.DTOS.Responses;

public class AllSponsorOrganisationsResponse
{
    public IEnumerable<SponsorOrganisationDto> SponsorOrganisations { get; set; } = [];
    public int TotalCount { get; set; }
}