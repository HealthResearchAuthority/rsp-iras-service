namespace Rsp.IrasService.Application.DTOS.Requests;

public class ReviewBodyDto
{
    public Guid Id { get; set; }
    public string OrganisationName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public List<string> Countries { get; set; } = [];
    public string Description { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string? UpdatedBy { get; set; }
}