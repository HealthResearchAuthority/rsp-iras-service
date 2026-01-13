using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

/// <summary>
/// Handles the status update of a project closure and associated changes by delegating to <see cref="IProjectClosureService"/>.
/// Uses primary constructor injection for the service dependency.
/// </summary>
public class UpdateProjectClosureStatusHandler(IProjectClosureService projectClosureService) : IRequestHandler<UpdateProjectClosureStatusCommand>
{
    /// <summary>
    /// Processes the <see cref="UpdateProjectClosureStatusCommand"/> and updates the specified project closure status and changes.
    /// </summary>
    /// <param name="request">The command containing the <see cref="Guid"/> of the project closure to update.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete. Not used by the service call.</param>
    public async Task Handle(UpdateProjectClosureStatusCommand request, CancellationToken cancellationToken)
    {
        // Delegate the removal to the application service. The service encapsulates the actual deletion logic.
        await projectClosureService.UpdateProjectClosureStatus(request.ProjectRecordId, request.Status, request.UserId);
    }
}