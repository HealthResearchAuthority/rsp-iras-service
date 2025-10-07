namespace Rsp.IrasService.Application.DTOS.Requests;

public class SponsorOrganisationSearchRequest
{
    public string? SearchQuery { get; set; }
    public List<string> Country { get; set; } = [];
    public bool? Status { get; set; }
}