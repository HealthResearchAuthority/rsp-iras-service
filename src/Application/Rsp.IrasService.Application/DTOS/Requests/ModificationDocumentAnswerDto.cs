namespace Rsp.IrasService.Application.DTOS.Requests;

/// <summary>
/// Request DTO representing a project modification.
/// </summary>
public class ModificationDocumentAnswerDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the modification.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Modification Document Id
    /// </summary>
    public Guid ModificationDocumentId { get; set; }

    /// <summary>
    /// Question Id
    /// </summary>
    public string QuestionId { get; set; } = null!;

    /// <summary>
    /// Version Id
    /// </summary>
    public string VersionId { get; set; } = null!;

    /// <summary>
    /// Question Category
    /// </summary>
    public string Category { get; set; } = null!;

    /// <summary>
    /// Question Section
    /// </summary>
    public string Section { get; set; } = null!;

    /// <summary>
    /// Question Response text
    /// </summary>
    public string Response { get; set; }

    /// <summary>
    /// Indiciates a single or multiple selection for the SelectionOption
    /// </summary>
    public string? OptionType { get; set; }

    /// <summary>
    /// Single selection answer e.g. Boolean (Yes/No)
    /// </summary>
    public string? SelectedOption { get; set; }

    /// <summary>
    /// Multiple answers
    /// </summary>
    public List<string> Responses { get; set; } = [];
}