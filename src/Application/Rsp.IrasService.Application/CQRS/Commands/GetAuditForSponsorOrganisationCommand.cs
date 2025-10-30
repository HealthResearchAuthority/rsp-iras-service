using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class GetAuditForSponsorOrganisationCommand(string rtsId) : IRequest<SponsorOrganisationAuditTrailResponse>
{
    public string RtsId { get; set; } = rtsId;
}