namespace Rsp.IrasService.Application.DTOS.Requests;

public class ProjectRecordSearchRequest
{
    public string? IrasId { get; set; }
    public string? ChiefInvestigatorName { get; set; }
    public List<string> LeadNation { get; set; } = [];
    public List<string> ParticipatingNation { get; set; } = [];
    public string? ShortProjectTitle { get; set; }
    public string? SponsorOrganisation { get; set; }
    public bool ActiveProjectsOnly { get; set; }
    public List<string> AllowedStatuses { get; set; } = [];
}