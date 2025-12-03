namespace Rsp.IrasService.Domain.Entities;

/// <summary>
/// Represents an answer to a project modification question.
/// </summary>
public class ProjectModificationAnswer : ProjectModificationAnswerBase
{
    /// <summary>
    /// Gets or sets the unique identifier for the project modification change.
    /// </summary>
    public Guid ProjectModificationId { get; set; }

    /// <summary>
    /// Navigation property to the related project record.
    /// </summary>
    public ProjectRecord? ProjectRecord { get; set; }

    /// <summary>
    /// Navigation property to the related project modification change.
    /// </summary>
    public ProjectModification? ProjectModification { get; set; }
}