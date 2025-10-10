using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class CreateSponsorOrganisationHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<CreateSponsorOrganisationCommand, SponsorOrganisationDto>
{
    public async Task<SponsorOrganisationDto> Handle(CreateSponsorOrganisationCommand request, CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.CreateSponsorOrganisation(request.CreateSponsorOrganisationRequest);
    }
}