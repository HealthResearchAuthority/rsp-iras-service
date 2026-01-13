namespace Rsp.Service.Domain.Entities;

public class ModificationParticipatingOrganisationAnswer
{
    /// <summary>
    /// Gets or sets the unique identifier for this modification document.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the modification participating organisation.
    /// </summary>
    public Guid ModificationParticipatingOrganisationId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the question being answered.
    /// </summary>
    public string QuestionId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the version Id for this question.
    /// </summary>
    public string VersionId { get; set; } = null!;

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
    /// Navigation property to the related modification participating organisation.
    /// </summary>
    public ModificationParticipatingOrganisation? ModificationParticipatingOrganisation { get; set; }
}