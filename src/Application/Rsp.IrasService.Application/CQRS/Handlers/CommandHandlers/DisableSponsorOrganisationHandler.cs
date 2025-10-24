using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class DisableSponsorOrganisationHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<DisableSponsorOrganisationCommand, SponsorOrganisationDto>
{
    public async Task<SponsorOrganisationDto?> Handle(DisableSponsorOrganisationCommand request, CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.DisableSponsorOrganisation(request.RtsId);
    }
}