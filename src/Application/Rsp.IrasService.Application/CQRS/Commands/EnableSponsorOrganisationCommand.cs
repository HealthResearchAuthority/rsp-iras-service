using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class EnableSponsorOrganisationCommand(string rtsId) : IRequest<SponsorOrganisationDto>
{
    public string RtsId { get; set; } = rtsId;
}