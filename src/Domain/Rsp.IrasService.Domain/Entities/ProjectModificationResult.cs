namespace Rsp.Service.Domain.Entities;

public class ProjectModificationResult : ModificationBase
{
    public string IrasId { get; set; } = null!;
    public int ModificationNumber { get; set; }
    public string? ReviewerId { get; set; }
}