using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

/// <summary>
/// Handler for retrieving modification area of changes for a project modification change.
/// </summary>
public class GetModificationAreaOfChangeHandler(IRespondentService respondentService) : IRequestHandler<GetModificationAreaOfChangeQuery, IEnumerable<ModificationAreaOfChangeDto>>
{
    /// <summary>
    /// Handles retrieval of respondent answers for a given modification change.
    /// </summary>
    /// <param name="request">The query containing the identifiers for the modification change and project.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of ModificationAreaOfChangeDtos.</returns>
    public async Task<IEnumerable<ModificationAreaOfChangeDto>> Handle(GetModificationAreaOfChangeQuery request, CancellationToken cancellationToken)
    {
        return await respondentService.GetModificationAreaOfChanges();
    }
}