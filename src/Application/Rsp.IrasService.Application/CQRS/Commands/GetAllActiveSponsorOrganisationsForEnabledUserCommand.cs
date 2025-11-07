using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class GetAllActiveSponsorOrganisationsForEnabledUserCommand(Guid userId) : IRequest<IEnumerable<SponsorOrganisationDto>>
{
    public Guid UserId { get; set; } = userId;
}