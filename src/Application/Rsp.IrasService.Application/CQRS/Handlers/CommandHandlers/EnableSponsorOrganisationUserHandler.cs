using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class EnableSponsorOrganisationUserHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<EnableSponsorOrganisationUserCommand, SponsorOrganisationUserDto>
{
    public async Task<SponsorOrganisationUserDto?> Handle(EnableSponsorOrganisationUserCommand request, CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.EnableUserInSponsorOrganisation(request.RtsId, request.UserId);
    }
}