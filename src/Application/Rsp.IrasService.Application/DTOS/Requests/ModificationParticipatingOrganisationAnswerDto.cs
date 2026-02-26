namespace Rsp.Service.Application.DTOS.Requests;

/// <summary>
/// Request DTO representing a project modification.
/// </summary>
public class ModificationParticipatingOrganisationAnswerDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the modification.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Modification Participating Organisation Id
    /// </summary>
    public Guid ModificationParticipatingOrganisationId { get; set; }

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
    public string ResponseText { get; set; }

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

    /// <summary>
    /// User Id of the respondent who provided the answer
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// Date and time when the answer was provided
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// User Id of the respondent who last updated the answer
    /// </summary>
    public string UpdatedBy { get; set; } = null!;

    /// <summary>
    /// Date and time when the answer was last updated
    /// </summary>
    public DateTime UpdatedDate { get; set; }
}