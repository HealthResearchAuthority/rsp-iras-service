namespace Rsp.QuestionSetService.Domain.Entities;

public class Respondent
{
    public string RespondentId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public List<string> Role { get; set; } = [];
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Version { get; set; }

    // Navigation properties
    public List<RespondentAnswer> RespondentAnswers { get; set; } = [];
}