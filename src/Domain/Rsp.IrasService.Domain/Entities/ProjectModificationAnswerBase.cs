namespace Rsp.IrasService.Domain.Entities;

/// <summary>
/// Represents an base answer to a project modification answer and modification change answer.
/// </summary>
public abstract class ProjectModificationAnswerBase
{
    /// <summary>
    /// Gets or sets the identifier of the question being answered.
    /// </summary>
    public string QuestionId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the version Id for this question.
    /// </summary>
    public string VersionId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the project personnel related to this answer.
    /// </summary>
    public string ProjectPersonnelId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the project record related to this answer.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the category of the question.
    /// </summary>
    public string Category { get; set; } = null!;

    /// <summary>
    /// Gets or sets the section of the question.
    /// </summary>
    public string Section { get; set; } = null!;

    /// <summary>
    /// Gets or sets the response provided to the question.
    /// </summary>
    public string? Response { get; set; }

    /// <summary>
    /// Gets or sets the type of option selected, if applicable.
    /// </summary>
    public string? OptionType { get; set; }

    /// <summary>
    /// Gets or sets the selected options, if applicable.
    /// </summary>
    public string? SelectedOptions { get; set; }
}