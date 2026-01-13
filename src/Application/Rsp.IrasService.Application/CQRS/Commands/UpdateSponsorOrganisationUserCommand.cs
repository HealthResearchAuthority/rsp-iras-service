using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class UpdateSponsorOrganisationUserCommand(SponsorOrganisationUserDto user) : IRequest<SponsorOrganisationUserDto>
{
    public SponsorOrganisationUserDto User { get; set; } = user;
}