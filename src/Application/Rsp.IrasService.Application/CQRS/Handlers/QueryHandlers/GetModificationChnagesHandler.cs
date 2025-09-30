using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

/// <summary>
/// Handles retrieval of all <see cref="ModificationChangeResponse"/> records
/// associated with a specific project modification.
/// </summary>
public class GetModificationChangesHandler(IProjectModificationService projectModificationService) : IRequestHandler<GetModificationChangesQuery, IEnumerable<ModificationChangeResponse>>
{
    /// <summary>
    /// Retrieves all modification changes for the supplied project modification identifier.
    /// </summary>
    /// <param name="request">
    /// The query containing the <see cref="GetModificationChangesQuery.ProjectModificationId"/> used to
    /// filter modification change records.
    /// </param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="ModificationChangeResponse"/> representing the
    /// modification changes linked to the specified project modification. If none exist, an empty sequence is returned.
    /// </returns>
    public async Task<IEnumerable<ModificationChangeResponse>> Handle(GetModificationChangesQuery request, CancellationToken cancellationToken)
    {
        // Delegate directly to the domain service (no additional transformation required here).
        return await projectModificationService.GetModificationChanges(request.ProjectModificationId);
    }
}