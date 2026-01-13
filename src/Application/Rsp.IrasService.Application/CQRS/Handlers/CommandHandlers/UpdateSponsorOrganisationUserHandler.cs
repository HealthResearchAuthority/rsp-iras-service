using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class UpdateSponsorOrganisationUserHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<UpdateSponsorOrganisationUserCommand, SponsorOrganisationUserDto?>
{
    public async Task<SponsorOrganisationUserDto?> Handle(UpdateSponsorOrganisationUserCommand request, CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.UpdateUserInSponsorOrganisation(request.User);
    }
}