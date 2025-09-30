namespace Rsp.IrasService.Application.DTOS.Requests;

public class ModificationDocumentDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the modification.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the related project modification change.
    /// </summary>
    public Guid ProjectModificationChangeId { get; set; }

    /// <summary>
    /// Gets or sets the project record identifier.
    /// </summary>
    public string ProjectRecordId { get; set; }

    /// <summary>
    /// Gets or sets the project personnel identifier.
    /// </summary>
    public string ProjectPersonnelId { get; set; }

    /// <summary>
    /// Gets or sets the document file name.
    /// </summary>
    public string FileName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the document storage path.
    /// </summary>
    public string? DocumentStoragePath { get; set; }

    /// <summary>
    /// Gets or sets the document file size.
    /// </summary>
    public long? FileSize { get; set; }

    /// <summary>
    /// Gets or sets the document scan status.
    /// </summary>
    public string? DocumentStatus { get; set; }

    /// <summary>
    /// Gets or sets the Corellation Id
    /// </summary>
    public string CorellationId { get; set; } = null!;
}