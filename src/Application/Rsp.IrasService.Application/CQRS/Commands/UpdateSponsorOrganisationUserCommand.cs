using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class UpdateSponsorOrganisationUserCommand(SponsorOrganisationUserDto user) : IRequest<SponsorOrganisationUserDto>
{
    public SponsorOrganisationUserDto User { get; set; } = user;
}