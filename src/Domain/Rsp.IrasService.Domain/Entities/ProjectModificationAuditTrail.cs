namespace Rsp.IrasService.Domain.Entities;

public class ProjectModificationAuditTrail
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProjectModificationId { get; set; }
    public DateTime DateTimeStamp { get; set; }
    public string Description { get; set; } = null!;
    public string User { get; set; } = null!;
    public bool IsBackstageOnly { get; set; }
    public bool ShowUserEmailToFrontstage { get; set; } = true;

    // navigation properties
    public ProjectModification ProjectModification { get; set; } = null!;
}