using Rsp.IrasService.Domain.Interfaces;

namespace Rsp.IrasService.Domain.Entities;

public class SponsorOrganisationUser : IAuditable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string RtsId { get; set; }
    public Guid UserId { get; set; }
    public string? Email { get; set; }
    public DateTime DateAdded { get; set; }
    public bool IsActive { get; set; } = true;
}