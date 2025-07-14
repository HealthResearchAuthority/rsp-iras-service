using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

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