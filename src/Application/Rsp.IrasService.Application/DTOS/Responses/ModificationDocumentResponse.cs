namespace Rsp.IrasService.Application.DTOS.Responses;

/// <summary>
/// Represents a response model for a modification document associated with a project change.
/// </summary>
public class ModificationDocumentResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the modification document record.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the associated project modification change.
    /// </summary>
    public Guid ProjectModificationChangeId { get; set; }

    /// <summary>
    /// Gets or sets the name of the uploaded file.
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the size of the file as a display string (e.g., "2.5 MB").
    /// </summary>
    public string FileSize { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier of the document type, if specified.
    /// </summary>
    public int? DocumentTypeId { get; set; }

    /// <summary>
    /// Gets or sets the version of the sponsor-provided document.
    /// </summary>
    public string? SponsorDocumentVersion { get; set; }

    /// <summary>
    /// Gets or sets the path where the document is stored in blob storage.
    /// </summary>
    public string? DocumentStoragePath { get; set; }

    /// <summary>
    /// Gets or sets the day part of the sponsor document date.
    /// </summary>
    public int? SponsorDocumentDateDay { get; set; }

    /// <summary>
    /// Gets or sets the month part of the sponsor document date.
    /// </summary>
    public int? SponsorDocumentDateMonth { get; set; }

    /// <summary>
    /// Gets or sets the year part of the sponsor document date.
    /// </summary>
    public int? SponsorDocumentDateYear { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a previous version of this document has been approved.
    /// </summary>
    public bool? HasPreviousVersionApproved { get; set; }
}