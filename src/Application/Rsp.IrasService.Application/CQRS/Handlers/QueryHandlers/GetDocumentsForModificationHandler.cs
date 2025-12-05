using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetDocumentsForModificationHandler(IProjectModificationService projectModificationService)
    : IRequestHandler<GetDocumentsForModificationQuery, ProjectOverviewDocumentResponse>
{
    public async Task<ProjectOverviewDocumentResponse> Handle(GetDocumentsForModificationQuery request, CancellationToken cancellationToken)
    {
        var response = await projectModificationService.GetDocumentsForModification
        (
            request.ModificationId,
            request.SearchQuery,
            request.PageNumber,
            request.PageSize,
            request.SortField,
            request.SortDirection
        );

        return response;
    }
}