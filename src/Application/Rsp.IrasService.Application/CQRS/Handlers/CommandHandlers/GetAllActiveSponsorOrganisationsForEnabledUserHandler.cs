using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class GetAllActiveSponsorOrganisationsForEnabledUserHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<GetAllActiveSponsorOrganisationsForEnabledUserCommand, IEnumerable<SponsorOrganisationDto>>
{
    public async Task<IEnumerable<SponsorOrganisationDto>> Handle(GetAllActiveSponsorOrganisationsForEnabledUserCommand request, CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.GetSponsorOrganisationsForUser(request.UserId);
    }
}