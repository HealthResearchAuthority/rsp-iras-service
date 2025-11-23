using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Extensions;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationsByIdsHandler(IProjectModificationService projectModificationService) : IRequestHandler<GetModificationsByIdsQuery, ModificationSearchResponse>
{
    public async Task<ModificationSearchResponse> Handle(GetModificationsByIdsQuery request, CancellationToken cancellationToken)
    {
        var response = await projectModificationService.GetModificationsByIds(request.Ids);

        return response.FilterByAllowedStatuses(request);
    }
}