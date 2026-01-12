using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

/// <summary>
/// Handles the deletion of a project by delegating to <see cref="IApplicationsService"/>.
/// Uses primary constructor injection for the service dependency.
/// </summary>
public class DeleteProjectHandler(IApplicationsService applicationsService) : IRequestHandler<DeleteProjectCommand>
{
    /// <summary>
    /// Processes the <see cref="DeleteModificationCommand"/> and deletes the specified modification status and changes.
    /// </summary>
    /// <param name="request">The command containing the <see cref="Guid"/> of the modification to delete.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete. Not used by the service call.</param>
    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        await applicationsService.DeleteProject(request.ProjectRecordId);
    }
}