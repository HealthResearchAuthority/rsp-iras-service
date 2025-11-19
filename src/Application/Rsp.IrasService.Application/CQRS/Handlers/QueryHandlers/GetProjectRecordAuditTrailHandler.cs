using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetProjectRecordAuditTrailHandler(IApplicationsService applicationsService) : IRequestHandler<GetProjectRecordAuditTrailQuery, ProjectRecordAuditTrailResponse>
{
    public async Task<ProjectRecordAuditTrailResponse> Handle(GetProjectRecordAuditTrailQuery request, CancellationToken cancellationToken)
    {
        return await applicationsService.GetProjectRecordAuditTrail(request.ProjectRecordId);
    }
}