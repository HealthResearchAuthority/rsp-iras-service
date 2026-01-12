using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Extensions;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationHandler(IProjectModificationService projectModificationService) : IRequestHandler<GetModificationQuery, ModificationResponse?>
{
    public async Task<ModificationResponse?> Handle(GetModificationQuery request, CancellationToken cancellationToken)
    {
        var modification = await projectModificationService.GetModification(request.ProjectRecordId, request.ProjectModificationId);

        return modification?.FilterSingleOrNull(request, m => m.Status);
    }
}