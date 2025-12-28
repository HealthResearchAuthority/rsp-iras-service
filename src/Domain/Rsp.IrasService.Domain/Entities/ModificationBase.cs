using Rsp.Service.Domain.Interfaces;

namespace Rsp.Service.Domain.Entities;

public class ModificationBase : ICreatable
{
    public string Id { get; set; } = null!;
    public string ProjectRecordId { get; set; } = null!;
    public string ModificationId { get; set; } = null!;
    public string ShortProjectTitle { get; set; } = null!;
    public string ModificationType { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string ReviewType { get; set; } = null!;
    public string ChiefInvestigatorFirstName { get; set; } = null!;
    public string ChiefInvestigatorLastName { get; set; } = null!;
    public string ChiefInvestigator { get; set; } = null!;
    public string LeadNation { get; set; } = null!;
    public string ParticipatingNation { get; set; } = null!;
    public string SponsorOrganisation { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime? SentToRegulatorDate { get; set; }
    public DateTime? SentToSponsorDate { get; set; }
    public string? ReviewerName { get; set; }
}