using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class DisableSponsorOrganisationHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<DisableSponsorOrganisationCommand, SponsorOrganisationDto>
{
    public async Task<SponsorOrganisationDto?> Handle(DisableSponsorOrganisationCommand request, CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.DisableSponsorOrganisation(request.RtsId);
    }
}