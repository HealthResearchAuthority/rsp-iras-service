using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class GetAllActiveSponsorOrganisationsForEnabledUserHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<GetAllActiveSponsorOrganisationsForEnabledUserCommand, IEnumerable<SponsorOrganisationDto>>
{
    public async Task<IEnumerable<SponsorOrganisationDto>> Handle(GetAllActiveSponsorOrganisationsForEnabledUserCommand request, CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.GetSponsorOrganisationsForUser(request.UserId);
    }
}