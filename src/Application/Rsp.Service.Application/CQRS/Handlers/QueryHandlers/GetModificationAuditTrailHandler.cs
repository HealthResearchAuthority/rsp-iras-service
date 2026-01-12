using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationAuditTrailHandler(IProjectModificationService projectModificationService) : IRequestHandler<GetModificationAuditTrailQuery, ModificationAuditTrailResponse>
{
    public async Task<ModificationAuditTrailResponse> Handle(GetModificationAuditTrailQuery request, CancellationToken cancellationToken)
    {
        return await projectModificationService.GetModificationAuditTrail(request.ProjectModificationId);
    }
}