using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.Specifications;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class GetAllActiveSponsorOrganisationsForEnabledUserHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<GetAllActiveSponsorOrganisationsForEnabledUserCommand, IEnumerable<SponsorOrganisationDto>>
{
    public async Task<IEnumerable<SponsorOrganisationDto>> Handle(GetAllActiveSponsorOrganisationsForEnabledUserCommand request, CancellationToken cancellationToken)
    {
        var spec = new GetActiveSponsorOrganisationsForEnabledUserSpecification(request.UserId);
        return await sponsorOrganisationsService.GetSponsorOrganisationsForSpecification(spec);
    }
}