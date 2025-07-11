﻿namespace Rsp.IrasService.Domain.Entities;

/// <summary>
/// Represents a project record, including its details and associated modifications.
/// </summary>
public class ProjectRecord
{
    /// <summary>
    /// Gets or sets the unique identifier for the project record.
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier for the project personnel associated with this record.
    /// </summary>
    public string ProjectPersonnelId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the title of the project.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the project.
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets a value indicating whether the project is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the status of the project.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the project record was created.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the project record was last updated.
    /// </summary>
    public DateTime UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the user who created the project record.
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who last updated the project record.
    /// </summary>
    public string UpdatedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the IRAS identifier associated with the project, if any.
    /// </summary>
    public int? IrasId { get; set; }

    /// <summary>
    /// Navigation property to the related project modifications associated with this project.
    /// </summary>
    public ICollection<ProjectModification> ProjectModifications { get; set; } = [];
}