namespace Rsp.Service.Domain.Entities;

public class ModificationDocumentsAuditTrail
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProjectModificationId { get; set; }
    public DateTime DateTimeStamp { get; set; }
    public string Description { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string User { get; set; } = null!;

    /// <summary>
    /// Navigation property to the related project modification change.
    /// </summary>
    public ProjectModification? ProjectModification { get; set; }
}