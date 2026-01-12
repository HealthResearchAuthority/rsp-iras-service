using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

/// <summary>
/// Handler for saving a modification change.
/// </summary>
public class SaveModificationChangeHandler(IProjectModificationService projectModificationService) : IRequestHandler<SaveModificationChangeCommand, ModificationChangeResponse>
{
    /// <summary>
    /// Handles saving or updating a modification change.
    /// </summary>
    /// <param name="request">The command containing the modification change details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the result of the operation.</returns>
    public async Task<ModificationChangeResponse> Handle(SaveModificationChangeCommand request, CancellationToken cancellationToken)
    {
        return await projectModificationService.CreateOrUpdateModificationChange(request.ModificationChangeRequest);
    }
}