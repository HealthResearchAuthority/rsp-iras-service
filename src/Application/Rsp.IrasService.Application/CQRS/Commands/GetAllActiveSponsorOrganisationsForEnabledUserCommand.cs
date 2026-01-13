using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class GetAllActiveSponsorOrganisationsForEnabledUserCommand(Guid userId) : IRequest<IEnumerable<SponsorOrganisationDto>>
{
    public Guid UserId { get; set; } = userId;
}