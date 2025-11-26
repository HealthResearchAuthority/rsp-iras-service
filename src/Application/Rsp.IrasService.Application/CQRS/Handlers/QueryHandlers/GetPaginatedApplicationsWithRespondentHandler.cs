using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Extensions;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetPaginatedApplicationsWithRespondentHandler(IApplicationsService applicationsService) : IRequestHandler<GetPaginatedApplicationsWithRespondentQuery, PaginatedResponse<ApplicationResponse>>
{
    public async Task<PaginatedResponse<ApplicationResponse>> Handle(GetPaginatedApplicationsWithRespondentQuery request, CancellationToken cancellationToken)
    {
        var result = await applicationsService.GetPaginatedRespondentApplications(request.RespondentId, request.SearchQuery, request.PageIndex, request.PageSize, request.SortField, request.SortDirection);

        return result.FilterByAllowedStatuses(request, a => a.Status);
    }
}