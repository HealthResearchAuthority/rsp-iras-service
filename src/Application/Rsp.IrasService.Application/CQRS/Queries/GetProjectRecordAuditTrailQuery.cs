using MediatR;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetProjectRecordAuditTrailQuery(string projectRecordId) : IRequest<ProjectRecordAuditTrailResponse>
{
    public string ProjectRecordId { get; set; } = projectRecordId;
}