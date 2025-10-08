namespace Rsp.IrasService.Application.DTOS.Requests;

public class SponsorOrganisationSearchRequest
{
    public bool? Status { get; set; }
    public List<string> RtsIds { get; set; } = [];
}