using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationAuditTrailHandler(IProjectModificationService projectModificationService) : IRequestHandler<GetModificationAuditTrailQuery, ModificationAuditTrailResponse>
{
    public async Task<ModificationAuditTrailResponse> Handle(GetModificationAuditTrailQuery request, CancellationToken cancellationToken)
    {
        return await projectModificationService.GetModificationAuditTrail(request.ProjectModificationId);
    }
}