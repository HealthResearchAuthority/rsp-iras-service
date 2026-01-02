using Rsp.IrasService.Domain.Attributes;
using Rsp.IrasService.Domain.Interfaces;

namespace Rsp.IrasService.Domain.Entities;

public class SponsorOrganisationUser : IAuditable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string RtsId { get; set; } = null!;
    public Guid UserId { get; set; }
    public string? Email { get; set; }
    public DateTime DateAdded { get; set; }

    [Auditable]
    public bool IsActive { get; set; }

    [Auditable]
    public bool IsAuthoriser { get; set; }

    [Auditable]
    public string SponsorRole { get; set; } = null!;
}