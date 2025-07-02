namespace Rsp.IrasService.Domain.Entities;

/// <summary>
/// Represents a person involved in a project, including their personal details and associated project records.
/// </summary>
public class ProjectPersonnel
{
    /// <summary>
    /// Gets or sets the unique identifier for the project personnel.
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Gets or sets the given (first) name of the project personnel.
    /// </summary>
    public string GivenName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the family (last) name of the project personnel.
    /// </summary>
    public string FamilyName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the email address of the project personnel.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Gets or sets the role of the project personnel within the project.
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// Gets or sets the collection of project records associated with this personnel.
    /// </summary>
    public ICollection<ProjectRecord> ProjectRecords { get; set; } = [];
}