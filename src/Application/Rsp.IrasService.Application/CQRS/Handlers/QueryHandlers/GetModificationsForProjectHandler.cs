using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Extensions;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationsForProjectHandler(IProjectModificationService projectModificationService) : IRequestHandler<GetModificationsForProjectQuery, ModificationSearchResponse>
{
    public async Task<ModificationSearchResponse> Handle(GetModificationsForProjectQuery request, CancellationToken cancellationToken)
    {
        var response = await projectModificationService.GetModificationsForProject
        (
            request.ProjectRecordId,
            request.SearchQuery,
            request.PageNumber,
            request.PageSize,
            request.SortField,
            request.SortDirection
        );

        return response.FilterByAllowedStatuses(request);
    }
}