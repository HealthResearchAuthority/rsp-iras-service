using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class AddSponsorOrganisationUserCommand(SponsorOrganisationUserDto sponsorOrganisationUserDto) : IRequest<SponsorOrganisationUserDto>
{
    public SponsorOrganisationUserDto SponsorOrganisationUserRequest { get; set; } = sponsorOrganisationUserDto;
}