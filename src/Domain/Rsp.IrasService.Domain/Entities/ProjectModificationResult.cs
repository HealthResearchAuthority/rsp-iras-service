namespace Rsp.IrasService.Domain.Entities;

public class ProjectModificationResult
{
    public string ModificationId { get; set; } = null!;
    public string IrasId { get; set; } = null!;
    public string ShortProjectTitle { get; set; } = null!;
    public string ModificationType { get; set; } = null!;
    public string ChiefInvestigator { get; set; } = null!;
    public string LeadNation { get; set; } = null!;
    public List<string> ParticipatingNation { get; set; } = null!;
    public string SponsorOrganisation { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}