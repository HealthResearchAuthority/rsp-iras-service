using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class AddSponsorOrganisationUserHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<AddSponsorOrganisationUserCommand, SponsorOrganisationUserDto>
{
    public async Task<SponsorOrganisationUserDto?> Handle(AddSponsorOrganisationUserCommand request, CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.AddUserToSponsorOrganisation(request.SponsorOrganisationUserRequest);
    }
}