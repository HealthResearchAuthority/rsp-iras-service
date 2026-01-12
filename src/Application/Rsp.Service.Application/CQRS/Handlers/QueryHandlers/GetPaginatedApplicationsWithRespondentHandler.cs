using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Extensions;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetPaginatedApplicationsWithRespondentHandler(IApplicationsService applicationsService) : IRequestHandler<GetPaginatedApplicationsWithRespondentQuery, PaginatedResponse<ApplicationResponse>>
{
    public async Task<PaginatedResponse<ApplicationResponse>> Handle(GetPaginatedApplicationsWithRespondentQuery request, CancellationToken cancellationToken)
    {
        var result = await applicationsService.GetPaginatedRespondentApplications(request.RespondentId, request.SearchQuery, request.PageIndex, request.PageSize, request.SortField, request.SortDirection);

        return result;
    }
}