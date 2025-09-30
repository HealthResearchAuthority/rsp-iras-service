using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

/// <summary>
/// Handles the removal of a modification change by delegating to <see cref="IProjectModificationService"/>.
/// Uses primary constructor injection for the service dependency.
/// </summary>
public class RemoveModificationChangeHandler(IProjectModificationService projectModificationService) : IRequestHandler<RemoveModificationChangeCommand>
{
    /// <summary>
    /// Processes the <see cref="RemoveModificationChangeCommand"/> and removes the specified modification change.
    /// </summary>
    /// <param name="request">The command containing the <see cref="Guid"/> of the modification change to remove.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete. Not used by the service call.</param>
    /// <returns>
    /// A <see cref="ReviewBodyUserDto"/> describing the affected user if available; otherwise <c>null</c>.
    /// </returns>
    public async Task Handle(RemoveModificationChangeCommand request, CancellationToken cancellationToken)
    {
        // Delegate the removal to the application service. The service encapsulates the actual deletion logic.
        await projectModificationService.RemoveModificationChange(request.ModificationChangeId);
    }
}