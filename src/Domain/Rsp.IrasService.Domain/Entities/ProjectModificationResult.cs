namespace Rsp.IrasService.Domain.Entities;

public class ProjectModificationResult
{
    public string Id { get; set; } = null!;
    public string ProjectRecordId { get; set; } = null!;
    public string ModificationId { get; set; } = null!;
    public string IrasId { get; set; } = null!;
    public int ModificationNumber { get; set; }
    public string ShortProjectTitle { get; set; } = null!;
    public string ModificationType { get; set; } = null!;
    public string ChiefInvestigator { get; set; } = null!;
    public string LeadNation { get; set; } = null!;
    public string ParticipatingNation { get; set; } = null!;
    public string SponsorOrganisation { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string? ReviewerId { get; set; } = null;
    public string? ReviewerName { get; set; } = null;
    public string Status { get; set; } = null!;
    public DateTime? SentToRegulatorDate { get; set; }
    public DateTime? SentToSponsorDate { get; set; }
    public DateTime? AuthorisedDate { get; set; }
}