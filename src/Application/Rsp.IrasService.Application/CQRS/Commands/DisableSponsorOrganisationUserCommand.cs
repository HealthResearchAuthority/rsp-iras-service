using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class DisableSponsorOrganisationUserCommand(string rtsId, Guid userId) : IRequest<SponsorOrganisationUserDto>
{
    public string RtsId { get; set; } = rtsId;
    public Guid UserId { get; set; } = userId;
}