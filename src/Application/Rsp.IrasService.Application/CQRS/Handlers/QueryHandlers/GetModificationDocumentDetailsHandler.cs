using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationDocumentDetailsHandler(IRespondentService respondentService) : IRequestHandler<GetModificationDocumentDetailsQuery, ModificationDocumentDto>
{
    /// <summary>
    /// Handles retrieval of respondent answers for a given modification change.
    /// </summary>
    /// <param name="request">The query containing the identifiers for the modification change and project.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of respondent answer DTOs.</returns>
    public async Task<ModificationDocumentDto> Handle(GetModificationDocumentDetailsQuery request, CancellationToken cancellationToken)
    {
        return await respondentService.GetModificationDocumentDetailsResponses(request.Id);
    }
}