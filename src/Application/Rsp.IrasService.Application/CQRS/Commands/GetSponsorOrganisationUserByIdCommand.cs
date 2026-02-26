using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class GetSponsorOrganisationUserByIdCommand(Guid sponsorOrganisationUserId, string rtsId) : IRequest<SponsorOrganisationUserDto>
{
    public Guid SponsorOrganisationUserId { get; set; } = sponsorOrganisationUserId;
    public string RtsId { get; set; } = rtsId;
}