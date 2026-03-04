using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetProjectDocumentsAuditTrailHandler(IDocumentService documentService)
    : IRequestHandler<GetProjectDocumentsAuditTrailQuery, ProjectDocumentsAuditTrailResponse>
{
    public async Task<ProjectDocumentsAuditTrailResponse> Handle(GetProjectDocumentsAuditTrailQuery request, CancellationToken cancellationToken)
    {
        var response = await documentService.GetProjectDocumentsAuditTrail
        (
            request.ProjectRecordId,
            request.PageNumber,
            request.PageSize,
            request.SortField,
            request.SortDirection
        );

        return response;
    }
}