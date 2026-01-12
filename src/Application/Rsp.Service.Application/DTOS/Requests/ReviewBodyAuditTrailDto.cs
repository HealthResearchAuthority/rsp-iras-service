namespace Rsp.Service.Application.DTOS.Requests;

public class ReviewBodyAuditTrailDto
{
    public Guid Id { get; set; }
    public Guid RegulatoryBodyId { get; set; }
    public DateTime DateTimeStamp { get; set; }
    public string Description { get; set; } = null!;
    public string User { get; set; } = null!;
}