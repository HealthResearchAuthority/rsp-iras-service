using System.ComponentModel.DataAnnotations;

namespace Rsp.IrasService.Domain.Entities;

public class SponsorOrganisationAuditTrail
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SponsorOrganisationId { get; set; }
    public string RtsId { get; set; }
    public DateTime DateTimeStamp { get; set; }
    public string Description { get; set; } = null!;
    public string User { get; set; } = null!;

    // navigation properties
    public SponsorOrganisation SponsorOrganisation { get; set; } = null!;
}