using MediatR;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Commands;

public class GetAuditForSponsorOrganisationCommand(string rtsId) : IRequest<SponsorOrganisationAuditTrailResponse>
{
    public string RtsId { get; set; } = rtsId;
}