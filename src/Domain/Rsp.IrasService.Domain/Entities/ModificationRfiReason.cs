using Rsp.Service.Domain.Interfaces;

namespace Rsp.Service.Domain.Entities;

public class ModificationRfiReason : IAuditable, ICreatable, IUpdatable
{
    public Guid Id { get; set; }
    public Guid ProjectModificationId { get; set; }
    public int Sequence { get; set; }
    public string Reason { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string UpdatedBy { get; set; } = null!;
}