namespace Rsp.IrasService.Domain.Entities;

/// <summary>
/// Represents an answer to a project modification question.
/// </summary>
public class ProjectModificationAnswer
{
    /// <summary>
    /// Gets or sets the unique identifier for the project modification change.
    /// </summary>
    public Guid ProjectModificationChangeId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the question being answered.
    /// </summary>
    public string QuestionId { get; set; } = null!;

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

    /// <summary>
    /// Navigation property to the related project personnel.
    /// </summary>
    public ProjectPersonnel? ProjectPersonnel { get; set; }

    /// <summary>
    /// Navigation property to the related project record.
    /// </summary>
    public ProjectRecord? ProjectRecord { get; set; }

    /// <summary>
    /// Navigation property to the related project modification change.
    /// </summary>
    public ProjectModificationChange? ProjectModificationChange { get; set; }
}