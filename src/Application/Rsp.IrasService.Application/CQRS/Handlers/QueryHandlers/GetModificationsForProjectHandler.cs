using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Extensions;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

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

        return response;
    }
}