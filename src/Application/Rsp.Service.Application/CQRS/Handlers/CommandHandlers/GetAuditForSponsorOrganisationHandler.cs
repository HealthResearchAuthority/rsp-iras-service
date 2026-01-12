using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class GetAuditForSponsorOrganisationHandler(ISponsorOrganisationsService sponsorOrganisationsService)
    : IRequestHandler<GetAuditForSponsorOrganisationCommand, SponsorOrganisationAuditTrailResponse>
{
    public async Task<SponsorOrganisationAuditTrailResponse> Handle(GetAuditForSponsorOrganisationCommand request,
        CancellationToken cancellationToken)
    {
        return await sponsorOrganisationsService.GetAuditTrailForSponsorOrganisation(request.RtsId);
    }
}