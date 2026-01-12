namespace Rsp.Service.Domain.Entities;

public class ProjectOverviewDocumentResult
{
    /// <summary>
    /// Gets or sets the unique identifier for this project overview.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the uploaded file.
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets if the scan is successful or not.
    /// </summary>
    public bool? IsMalwareScanSuccessful { get; set; }

    /// <summary>
    /// Gets or sets the name of the document, if specified.
    /// </summary>
    public string DocumentName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the path where the document is stored in blob storage.
    /// </summary>
    public string? DocumentStoragePath { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the document type, if specified.
    /// </summary>
    public string DocumentType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier of the document version, if specified.
    /// </summary>
    public string DocumentVersion { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier of the document date, if specified.
    /// </summary>
    public DateTime? DocumentDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the status, if specified.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier of the modification, if specified.
    /// </summary>
    public string ModificationIdentifier { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of the modification.
    /// </summary>
    public int ModificationNumber { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for this modification.
    /// </summary>
    public Guid ProjectModificationId { get; set; }
}