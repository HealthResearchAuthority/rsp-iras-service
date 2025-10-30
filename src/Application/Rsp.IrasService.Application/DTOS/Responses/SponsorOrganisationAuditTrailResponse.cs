using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.DTOS.Responses;

public class SponsorOrganisationAuditTrailResponse
{
    public IEnumerable<SponsorOrganisationAuditTrailDto> Items { get; set; } = Enumerable.Empty<SponsorOrganisationAuditTrailDto>();
    public int TotalCount { get; set; }
}