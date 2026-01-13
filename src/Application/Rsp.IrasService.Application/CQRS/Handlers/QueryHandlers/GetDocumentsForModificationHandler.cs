using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

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