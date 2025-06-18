namespace Rsp.IrasService.Domain.Entities;

public class RegulatoryBodyUsers
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public DateTime DateAdded { get; set; }
}