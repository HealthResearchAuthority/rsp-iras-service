using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class CreateSponsorOrganisationCommand(SponsorOrganisationDto sponsorOrganisationDto) : IRequest<SponsorOrganisationDto>
{
    public SponsorOrganisationDto CreateSponsorOrganisationRequest { get; set; } = sponsorOrganisationDto;
}