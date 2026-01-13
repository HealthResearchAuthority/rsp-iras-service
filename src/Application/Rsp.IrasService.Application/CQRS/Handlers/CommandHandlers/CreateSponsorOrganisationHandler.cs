using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class CreateSponsorOrganisationHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<CreateSponsorOrganisationCommand, SponsorOrganisationDto>
{
    public async Task<SponsorOrganisationDto> Handle(CreateSponsorOrganisationCommand request, CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.CreateSponsorOrganisation(request.CreateSponsorOrganisationRequest);
    }
}