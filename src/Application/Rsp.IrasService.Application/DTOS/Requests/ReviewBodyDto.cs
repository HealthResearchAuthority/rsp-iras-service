namespace Rsp.IrasService.Application.DTOS.Requests;

public class ReviewBodyDto :BaseDto
{
    public string OrganisationName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public List<string> Countries { get; set; } = [];
    public string? Description { get; set; }

}