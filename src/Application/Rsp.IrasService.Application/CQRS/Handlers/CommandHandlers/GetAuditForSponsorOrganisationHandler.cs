using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class GetAuditForSponsorOrganisationHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<GetAuditForSponsorOrganisationCommand, SponsorOrganisationAuditTrailResponse>
{
    public async Task<SponsorOrganisationAuditTrailResponse> Handle(GetAuditForSponsorOrganisationCommand request,
        CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.GetAuditTrailForSponsorOrganisation(request.RtsId);
    }
}