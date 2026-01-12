using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetProjectRecordAuditTrailHandler(IApplicationsService applicationsService) : IRequestHandler<GetProjectRecordAuditTrailQuery, ProjectRecordAuditTrailResponse>
{
    public async Task<ProjectRecordAuditTrailResponse> Handle(GetProjectRecordAuditTrailQuery request, CancellationToken cancellationToken)
    {
        return await applicationsService.GetProjectRecordAuditTrail(request.ProjectRecordId);
    }
}