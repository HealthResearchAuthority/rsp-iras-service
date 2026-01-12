using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetDocumentsForProjectOverviewHandler(IProjectModificationService projectModificationService)
    : IRequestHandler<GetDocumentsForProjectOverviewQuery, ProjectOverviewDocumentResponse>
{
    public async Task<ProjectOverviewDocumentResponse> Handle(GetDocumentsForProjectOverviewQuery request, CancellationToken cancellationToken)
    {
        var response = await projectModificationService.GetDocumentsForProjectOverview
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