namespace Rsp.IrasService.Domain.Entities;

public class ProjectApplicationRespondentAnswer
{
    public string Id { get; set; } = null!;
    public string ProjectApplicationId { get; set; } = null!;
    public string QuestionId { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Section { get; set; } = null!;
    public string? Response { get; set; }
    public string? OptionType { get; set; } = null!;
    public string? SelectedOptions { get; set; }
    public ProjectApplicationRespondent? ProjectApplicationRespondent { get; set; }
    public ProjectApplication? ProjectApplication { get; set; }
}