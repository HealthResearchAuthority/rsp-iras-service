using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetPaginatedProjectRecordsHandler(IApplicationsService applicationsService) : IRequestHandler<GetPaginatedProjectRecordsQuery, PaginatedResponse<CompleteProjectRecordResponse>>
{
    public async Task<PaginatedResponse<CompleteProjectRecordResponse>> Handle(GetPaginatedProjectRecordsQuery request, CancellationToken cancellationToken)
    {
        return await applicationsService.GetPaginatedApplications(
            request.SearchQuery,
            request.PageIndex,
            request.PageSize,
            request.SortField,
            request.SortDirection);
    }
}