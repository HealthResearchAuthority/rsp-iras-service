namespace Rsp.IrasService.Domain.Entities;

public class Respondent
{
    public string RespondentId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<string> Role { get; set; } = [];
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Version { get; set; }
    public ICollection<ResearchApplication> ResearchApplications { get; set; } = [];
}