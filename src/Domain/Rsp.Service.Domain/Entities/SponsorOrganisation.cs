using Rsp.Service.Domain.Attributes;
using Rsp.Service.Domain.Interfaces;

namespace Rsp.Service.Domain.Entities;

public class SponsorOrganisation : IAuditable
{
    public Guid Id { get; set; }

    public string RtsId { get; set; }

    [Auditable]
    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string? UpdatedBy { get; set; }

    // navigation properties
    public ICollection<SponsorOrganisationUser> Users { get; set; } = [];
}