namespace Rsp.IrasService.Domain.Entities;

public class ProjectRecordAuditTrail
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ProjectRecordId { get; set; } = null!;
    public DateTime DateTimeStamp { get; set; }
    public string Description { get; set; } = null!;
    public string User { get; set; } = null!;

    // navigation properties
    public ProjectRecord ProjectRecord { get; set; } = null!;
}