namespace Rsp.Service.Domain.Entities;

public class ModificationDocument
{
    /// <summary>
    /// Gets or sets the unique identifier for this modification document.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the project modification.
    /// </summary>
    public Guid ProjectModificationId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user related to this document.
    /// </summary>
    public string UserId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the project record related to this document.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

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
    /// Gets or sets the current status of this document (e.g., Pending, Approved, Rejected).
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets if the scan is successful or not.
    /// </summary>
    public bool? IsMalwareScanSuccessful { get; set; }

    /// <summary>
    /// Gets or sets the document created date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// The current document replaces the document identified by this Id.
    /// </summary>
    public Guid? ReplacesDocumentId { get; set; }

    /// <summary>
    /// The current document is replaced by the document identified by this Id
    /// </summary>
    public Guid? ReplacedByDocumentId { get; set; }

    public ModificationDocument? ReplacedByDocument { get; set; }

    /// <summary>
    /// This field will indicate whether the document is CLEAN or TRACKED
    /// </summary>
    public string? DocumentType { get; set; }

    /// <summary>
    /// For a CLEAN document: reference to the corresponding TRACKED version.
    /// For a TRACKED document: reference to the corresponding CLEAN version.
    /// </summary>
    public Guid? LinkedDocumentId { get; set; }

    public ModificationDocument? LinkedDocument { get; set; }

    /// <summary>
    /// Navigation property to the related project record.
    /// </summary>
    public ProjectRecord? ProjectRecord { get; set; }

    /// <summary>
    /// Navigation property to the related project modification change.
    /// </summary>
    public ProjectModification? ProjectModification { get; set; }
}