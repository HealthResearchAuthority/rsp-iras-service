using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.DTOS.Responses;

public class AllSponsorOrganisationsResponse
{
    public IEnumerable<SponsorOrganisationDto> SponsorOrganisations { get; set; } = [];
    public int TotalCount { get; set; }
}