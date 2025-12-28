using Rsp.IrasService.Domain.Attributes;
using Rsp.IrasService.Domain.Interfaces;

namespace Rsp.IrasService.Domain.Entities;

public class SponsorOrganisation : IAuditable, ICreatable, IUpdatable
{
    public Guid Id { get; set; }

    public string RtsId { get; set; } = null!;

    [Auditable]
    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string UpdatedBy { get; set; } = null!;

    // navigation properties
    public ICollection<SponsorOrganisationUser> Users { get; set; } = [];
}