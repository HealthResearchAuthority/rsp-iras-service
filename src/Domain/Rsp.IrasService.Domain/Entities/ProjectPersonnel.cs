namespace Rsp.IrasService.Domain.Entities;

public class ProjectPersonnel
{
    public string Id { get; set; } = null!;
    public string GivenName { get; set; } = null!;
    public string FamilyName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Role { get; set; }
    public ICollection<ProjectRecord> ProjectRecords { get; set; } = [];
}