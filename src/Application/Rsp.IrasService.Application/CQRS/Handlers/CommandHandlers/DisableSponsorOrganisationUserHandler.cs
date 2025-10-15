using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class DisableSponsorOrganisationUserHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<DisableSponsorOrganisationUserCommand, SponsorOrganisationUserDto>
{
    public async Task<SponsorOrganisationUserDto?> Handle(DisableSponsorOrganisationUserCommand request, CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.DisableUserInSponsorOrganisation(request.RtsId, request.UserId);
    }
}