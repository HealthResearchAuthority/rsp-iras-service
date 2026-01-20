using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class GetSponsorOrganisationUserByIdCommand(Guid sponsorOrganisationUserId) : IRequest<SponsorOrganisationUserDto>
{
    public Guid SponsorOrganisationUserId { get; set; } = sponsorOrganisationUserId;
}