using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.Extensions;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

/// <summary>
/// Handler for retrieving modification answers for a project modification change.
/// </summary>
public class GetModificationDocumentsByTypeHandler(IRespondentService respondentService) : IRequestHandler<GetModificationDocumentsByTypeQuery, IEnumerable<ModificationDocumentDto>>
{
    /// <summary>
    /// Handles retrieval of respondent answers for a given modification change.
    /// </summary>
    /// <param name="request">The query containing the identifiers for the modification change and project.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of respondent answer DTOs.</returns>
    public async Task<IEnumerable<ModificationDocumentDto>> Handle(GetModificationDocumentsByTypeQuery request, CancellationToken cancellationToken)
    {
        var docs = await respondentService.GetDocumentsByType(request!.ProjectRecordId, request!.DocumentTypeId);

        return docs.FilterByAllowedStatuses(request, d => d.Status);
    }
}