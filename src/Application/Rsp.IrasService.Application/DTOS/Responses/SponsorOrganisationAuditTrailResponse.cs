using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.DTOS.Responses;

public class SponsorOrganisationAuditTrailResponse
{
    public IEnumerable<SponsorOrganisationAuditTrailDto> Items { get; set; } = Enumerable.Empty<SponsorOrganisationAuditTrailDto>();
    public int TotalCount { get; set; }
}