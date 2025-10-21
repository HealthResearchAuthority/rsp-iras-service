using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class EnableSponsorOrganisationHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<EnableSponsorOrganisationCommand, SponsorOrganisationDto>
{
    public async Task<SponsorOrganisationDto?> Handle(EnableSponsorOrganisationCommand request, CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.EnableSponsorOrganisation(request.RtsId);
    }
}