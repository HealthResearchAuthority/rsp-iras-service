using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Extensions;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetPaginatedProjectRecordsHandler(IApplicationsService applicationsService) : IRequestHandler<GetPaginatedProjectRecordsQuery, PaginatedResponse<CompleteProjectRecordResponse>>
{
    public async Task<PaginatedResponse<CompleteProjectRecordResponse>> Handle(GetPaginatedProjectRecordsQuery request, CancellationToken cancellationToken)
    {
        var result = await applicationsService.GetPaginatedApplications(
            request.SearchQuery,
            request.PageIndex,
            request.PageSize,
            request.SortField,
            request.SortDirection);

        return result;
    }
}