using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationsHandler(IProjectModificationService projectModificationService) : IRequestHandler<GetModificationsQuery, ModificationSearchResponse>
{
    public async Task<ModificationSearchResponse> Handle(GetModificationsQuery request, CancellationToken cancellationToken)
    {
        return await projectModificationService.GetModifications
        (
            request.SearchQuery,
            request.PageNumber,
            request.PageSize,
            request.SortField,
            request.SortDirection
        );
    }
}