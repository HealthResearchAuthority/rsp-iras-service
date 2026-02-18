using Rsp.Service.Domain.Interfaces;

namespace Rsp.Service.Domain.Entities;

public class ModificationRfiReason : IAuditable
{
    public Guid Id { get; set; }
    public Guid ProjectModificationId { get; set; }
    public int Sequence { get; set; }
    public string Reason { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
}