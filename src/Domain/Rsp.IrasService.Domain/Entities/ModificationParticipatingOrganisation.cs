namespace Rsp.IrasService.Domain.Entities;

public class ModificationParticipatingOrganisation
{
    /// <summary>
    /// Gets or sets the unique identifier for this modification document.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the project modification change.
    /// </summary>
    public Guid ProjectModificationChangeId { get; set; }

    /// <summary>
    /// Gets or sets the organisation Id.
    /// </summary>
    public string OrganisationId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the user related to this document.
    /// </summary>
    public string UserId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the project record related to this document.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// Navigation property to the related project record.
    /// </summary>
    public ProjectRecord? ProjectRecord { get; set; }

    /// <summary>
    /// Navigation property to the related project modification change.
    /// </summary>
    public ProjectModificationChange? ProjectModificationChange { get; set; }
}