using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class DisableSponsorOrganisationCommand(string rtsId) : IRequest<SponsorOrganisationDto>
{
    public string RtsId { get; set; } = rtsId;
}