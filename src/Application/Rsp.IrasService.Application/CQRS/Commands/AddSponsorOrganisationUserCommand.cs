using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class AddSponsorOrganisationUserCommand(SponsorOrganisationUserDto sponsorOrganisationUserDto) : IRequest<SponsorOrganisationUserDto>
{
    public SponsorOrganisationUserDto SponsorOrganisationUserRequest { get; set; } = sponsorOrganisationUserDto;
}