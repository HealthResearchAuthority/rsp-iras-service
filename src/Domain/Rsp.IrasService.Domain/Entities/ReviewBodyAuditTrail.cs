namespace Rsp.IrasService.Domain.Entities;

public class ReviewBodyAuditTrail
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ReviewBodyId { get; set; }
    public DateTime DateTimeStamp { get; set; }
    public string Description { get; set; } = null!;
    public string User { get; set; } = null!;

    // navigation properties
    public ReviewBody ReviewBody { get; set; } = null!;
}