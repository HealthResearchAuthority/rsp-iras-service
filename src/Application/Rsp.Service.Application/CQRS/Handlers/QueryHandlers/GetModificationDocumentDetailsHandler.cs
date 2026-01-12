using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.Extensions;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationDocumentDetailsHandler(IRespondentService respondentService) : IRequestHandler<GetModificationDocumentDetailsQuery, ModificationDocumentDto?>
{
    /// <summary>
    /// Handles retrieval of respondent answers for a given modification change.
    /// </summary>
    /// <param name="request">The query containing the identifiers for the modification change and project.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of respondent answer DTOs.</returns>
    public async Task<ModificationDocumentDto?> Handle(GetModificationDocumentDetailsQuery request, CancellationToken cancellationToken)
    {
        var doc = await respondentService.GetModificationDocumentDetailsResponses(request.Id);

        return doc?.FilterSingleOrNull(request, d => d.Status);
    }
}