namespace Rsp.IrasService.Application.DTOS.Requests;

public class SponsorOrganisationAuditTrailDto
{
    public Guid Id { get; set; }

    public Guid SponsorOrganisationId { get; set; }
    public string RtsId { get; set; }
    public DateTime DateTimeStamp { get; set; }
    public string Description { get; set; } = null!;
    public string User { get; set; } = null!;
}