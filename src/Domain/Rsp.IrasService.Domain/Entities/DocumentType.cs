namespace Rsp.IrasService.Domain.Entities;

public class DocumentType
{
    /// <summary>
    /// Gets or sets the unique identifier for this document type.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the document type.
    /// </summary>
    public string Name { get; set; } = null!;
}