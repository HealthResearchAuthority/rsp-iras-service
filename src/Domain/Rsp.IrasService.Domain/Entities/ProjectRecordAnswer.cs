namespace Rsp.IrasService.Domain.Entities;

public class ProjectRecordAnswer
{
    public string ProjectPersonnelId { get; set; } = null!;
    public string ProjectRecordId { get; set; } = null!;
    public string QuestionId { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Section { get; set; } = null!;
    public string? Response { get; set; }
    public string? OptionType { get; set; } = null!;
    public string? SelectedOptions { get; set; }
    public ProjectPersonnel? ProjectPersonnel { get; set; }
    public ProjectRecord? ProjectRecord { get; set; }
}