namespace Rsp.Service.Application.DTOS.Requests;

public class SponsorOrganisationDto : BaseDto
{
    public string RtsId { get; set; } = null!;
    public IEnumerable<SponsorOrganisationUserDto>? Users { get; set; }
}