using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetProjectRecordAuditTrailQuery(string projectRecordId) : IRequest<ProjectRecordAuditTrailResponse>
{
    public string ProjectRecordId { get; set; } = projectRecordId;
}