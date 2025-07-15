using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationsHandler(IApplicationsService applicationsService) : IRequestHandler<GetModificationsQuery, ModificationResponse>
{
    public async Task<ModificationResponse> Handle(GetModificationsQuery request, CancellationToken cancellationToken)
    {
        return await applicationsService.GetModifications
        (
            request.SearchQuery,
            request.PageNumber,
            request.PageSize,
            request.SortField,
            request.SortDirection
        );
    }
}