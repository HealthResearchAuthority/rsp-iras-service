using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class DisableSponsorOrganisationCommand(string rtsId) : IRequest<SponsorOrganisationDto>
{
    public string RtsId { get; set; } = rtsId;
}