using Rsp.IrasService.Domain.Attributes;
using Rsp.IrasService.Domain.Interfaces;

namespace Rsp.IrasService.Domain.Entities;

public class SponsorOrganisation : IAuditable
{
    public Guid Id { get; set; }
    [Auditable]
    public string SponsorOrganisationName { get; set; } = null!;
    [Auditable]
    public List<string> Countries { get; set; } = [];
    [Auditable]
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string? UpdatedBy { get; set; }

    // navigation properties
    public ICollection<SponsorOrganisationUser> Users { get; set; } = [];
}