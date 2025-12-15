using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

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