using Rsp.Service.Domain.Attributes;
using Rsp.Service.Domain.Interfaces;

namespace Rsp.Service.Domain.Entities;

public class RegulatoryBody : IAuditable
{
    public Guid Id { get; set; }

    [Auditable]
    public string RegulatoryBodyName { get; set; } = null!;

    [Auditable]
    public string EmailAddress { get; set; } = null!;

    [Auditable]
    public List<string> Countries { get; set; } = [];

    [Auditable]
    public string? Description { get; set; }

    [Auditable]
    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string? UpdatedBy { get; set; }

    // navigation properties
    public ICollection<RegulatoryBodyUser> Users { get; set; } = [];
}