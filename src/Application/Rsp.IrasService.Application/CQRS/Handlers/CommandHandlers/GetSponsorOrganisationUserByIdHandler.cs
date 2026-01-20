using MediatR;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class GetSponsorOrganisationUserByIdHandler(ISponsorOrganisationsService sponsorOrganisationService)
    : IRequestHandler<GetSponsorOrganisationUserByIdCommand, SponsorOrganisationUserDto>
{
    public async Task<SponsorOrganisationUserDto> Handle(GetSponsorOrganisationUserByIdCommand request, CancellationToken cancellationToken)
    {
        return await sponsorOrganisationService.GetSponsorOrganisationUserById(request.SponsorOrganisationUserId);
    }
}