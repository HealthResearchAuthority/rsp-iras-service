using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationsByIdsHandler(IProjectModificationService projectModificationService) : IRequestHandler<GetModificationsByIdsQuery, ModificationResponse>
{
    public async Task<ModificationResponse> Handle(GetModificationsByIdsQuery request, CancellationToken cancellationToken)
    {
        return await projectModificationService.GetModificationsByIds(request.Ids);
    }
}