namespace Rsp.Service.Domain.Entities;

public abstract class ProjectRecordAnswerBase
{
    public string ProjectRecordId { get; set; } = null!;
    public string QuestionId { get; set; } = null!;
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string VersionId { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Section { get; set; } = null!;
    public string? Response { get; set; }
    public string? OptionType { get; set; }
    public string? SelectedOptions { get; set; }
    public string UpdatedBy { get; set; } = null!;
    public DateTime UpdatedDate { get; set; }
}