using Rsp.Service.Domain.Interfaces;

namespace Rsp.Service.Domain.Entities;

public class ModificationParticipatingOrganisation : ICreatable, IUpdatable
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
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date and time when this record was created (in UTC).
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last updated this record
    /// </summary>
    public string UpdatedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date and time when this record was last updated (in UTC).
    /// </summary>
    public DateTime UpdatedDate { get; set; }

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