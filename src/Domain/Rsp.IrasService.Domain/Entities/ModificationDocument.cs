﻿namespace Rsp.IrasService.Domain.Entities;

public class ModificationDocument
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
    /// Gets or sets the identifier of the project personnel related to this document.
    /// </summary>
    public string ProjectPersonnelId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the project record related to this document.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the unique identifier for this document type.
    /// </summary>
    public Guid? DocumentTypeId { get; set; }

    /// <summary>
    /// Gets or sets the document storage path.
    /// </summary>
    public string? DocumentStoragePath { get; set; }

    /// <summary>
    /// Gets or sets the name of the document.
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Gets or sets the size of the document.
    /// </summary>
    public int? FileSize { get; set; }

    /// <summary>
    /// Gets or sets the version of the document.
    /// </summary>
    public string? SponsorDocumentVersion { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this document has previous version.
    /// </summary>
    public bool? HasPreviousVersion { get; set; }

    /// <summary>
    /// Gets or sets the date and time for the sponsor document.
    /// </summary>
    public DateTime? SponsorDocumentDate { get; set; }

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

    /// <summary>
    /// Navigation property to the related document type.
    /// </summary>
    public DocumentType? DocumentType { get; set; }
}