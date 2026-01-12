using Rsp.Service.Domain.Interfaces;

namespace Rsp.Service.Domain.Entities;

public class RegulatoryBodyUser : IAuditable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string? Email { get; set; }
    public DateTime DateAdded { get; set; }
}