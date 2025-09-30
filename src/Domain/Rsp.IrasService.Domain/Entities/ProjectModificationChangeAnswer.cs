namespace Rsp.IrasService.Domain.Entities;

/// <summary>
/// Represents an answer to a project modification question.
/// </summary>
public class ProjectModificationChangeAnswer : ProjectModificationAnswerBase
{
    /// <summary>
    /// Gets or sets the unique identifier for the project modification change.
    /// </summary>
    public Guid ProjectModificationChangeId { get; set; }

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