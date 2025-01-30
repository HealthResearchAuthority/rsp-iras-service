namespace Rsp.IrasService.Domain.Entities;

[ExcludeFromCodeCoverage]
public class RespondentAnswer
{
    public string RespondentId { get; set; } = null!;
    public string ApplicationId { get; set; } = null!;
    public string QuestionId { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Section { get; set; } = null!;
    public string? Response { get; set; }
    public string? OptionType { get; set; } = null!;
    public string? SelectedOptions { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Version { get; set; }
    public Respondent? Respondent { get; set; }
    public ResearchApplication? ResearchApplication { get; set; }
}