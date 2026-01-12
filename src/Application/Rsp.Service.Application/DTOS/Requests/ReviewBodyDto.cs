namespace Rsp.Service.Application.DTOS.Requests;

public class ReviewBodyDto : BaseDto
{
    public string RegulatoryBodyName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public List<string> Countries { get; set; } = [];
    public string? Description { get; set; }
    public IEnumerable<ReviewBodyUserDto>? Users { get; set; }

    public string CommaSeperatedCountries
    {
        get => string.Join(',', Countries);
    }
}