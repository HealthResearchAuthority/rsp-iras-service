using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class GetSponsorOrganisationUserHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<GetSponsorOrganisationUserCommand, SponsorOrganisationUserDto>
{
    public async Task<SponsorOrganisationUserDto?> Handle(GetSponsorOrganisationUserCommand request, CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.GetUserInSponsorOrganisation(request.RtsId, request.UserId);
    }
}