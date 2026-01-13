using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

/// <summary>
/// Handler responsible for retrieving a single modification change by its unique identifier.
/// </summary>
public class GetModificationChangeHandler(IProjectModificationService projectModificationService) : IRequestHandler<GetModificationChangeQuery, ModificationChangeResponse>
{
    /// <summary>
    /// Processes the query to retrieve a modification change.
    /// </summary>
    /// <param name="request">The query containing the modification change identifier.</param>
    /// <param name="cancellationToken">A cancellation token used to cancel the operation.</param>
    /// <returns>The <see cref="ModificationChangeResponse"/> representing the requested modification change.</returns>
    public async Task<ModificationChangeResponse> Handle(GetModificationChangeQuery request, CancellationToken cancellationToken)
    {
        return await projectModificationService.GetModificationChange(request.ModificationChangeId);
    }
}