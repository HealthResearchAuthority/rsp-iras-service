using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class UpdateSponsorOrganisationUserHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<UpdateSponsorOrganisationUserCommand, SponsorOrganisationUserDto?>
{
    public async Task<SponsorOrganisationUserDto?> Handle(UpdateSponsorOrganisationUserCommand request, CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.UpdateUserInSponsorOrganisation(request.User);
    }
}