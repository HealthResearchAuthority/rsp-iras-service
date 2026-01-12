using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

/// <summary>
/// Handler for creating a new project modification.
/// </summary>
public class CreateModificationHandler(IProjectModificationService projectModificationService) : IRequestHandler<CreateModificationCommand, ModificationResponse>
{
    /// <summary>
    /// Handles the creation of a new modification.
    /// </summary>
    /// <param name="request">The create modification command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the created modification details.</returns>
    public async Task<ModificationResponse> Handle(CreateModificationCommand request, CancellationToken cancellationToken)
    {
        return await projectModificationService.CreateModification(request.ModificationRequest);
    }
}