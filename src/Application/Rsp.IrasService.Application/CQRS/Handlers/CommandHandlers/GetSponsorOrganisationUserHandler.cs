using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class GetSponsorOrganisationUserHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<GetSponsorOrganisationUserCommand, SponsorOrganisationUserDto>
{
    public async Task<SponsorOrganisationUserDto?> Handle(GetSponsorOrganisationUserCommand request, CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.GetUserInSponsorOrganisation(request.RtsId, request.UserId);
    }
}