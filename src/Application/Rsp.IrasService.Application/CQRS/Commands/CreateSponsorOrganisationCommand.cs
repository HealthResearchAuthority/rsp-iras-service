using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class CreateSponsorOrganisationCommand(SponsorOrganisationDto sponsorOrganisationDto) : IRequest<SponsorOrganisationDto>
{
    public SponsorOrganisationDto CreateSponsorOrganisationRequest { get; set; } = sponsorOrganisationDto;
}