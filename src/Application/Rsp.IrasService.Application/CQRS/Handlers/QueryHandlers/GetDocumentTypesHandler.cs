using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

/// <summary>
/// Handler for retrieving modification answers for a project modification change.
/// </summary>
public class GetDocumentTypesHandler(IRespondentService respondentService) : IRequestHandler<GetDocumentTypesQuery, IEnumerable<DocumentTypeResponse>>
{
    /// <summary>
    /// Handles retrieval of respondent answers for a given modification change.
    /// </summary>
    /// <param name="request">The query containing the identifiers for the modification change and project.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of respondent answer DTOs.</returns>
    public async Task<IEnumerable<DocumentTypeResponse>> Handle(GetDocumentTypesQuery request, CancellationToken cancellationToken)
    {
        return await respondentService.GetDocumentTypeResponses();
    }
}