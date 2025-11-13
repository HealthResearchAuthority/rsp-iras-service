using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

/// <summary>
/// Handles the status update of a modification and associated changes by delegating to <see cref="IProjectModificationService"/>.
/// Uses primary constructor injection for the service dependency.
/// </summary>
public class UpdateModificationHandler(IProjectModificationService projectModificationService) : IRequestHandler<UpdateModificationCommand>
{
    /// <summary>
    /// Processes the <see cref="UpdateModificationCommand"/> and updates the specified modification status and changes.
    /// </summary>
    /// <param name="request">The command containing the <see cref="Guid"/> of the modification to update.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete. Not used by the service call.</param>
    public async Task Handle(UpdateModificationCommand request, CancellationToken cancellationToken)
    {
        // Delegate the removal to the application service. The service encapsulates the actual deletion logic.
        await projectModificationService.UpdateModification(request.ModificationRequest);
    }
}