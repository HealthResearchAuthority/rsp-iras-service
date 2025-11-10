using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

/// <summary>
/// Handler for assigning modifications to a reviewer.
/// </summary>
public class AssignModificationToReviewerHandler(IProjectModificationService projectModificationService) : IRequestHandler<AssignModificationsToReviewerCommand>
{
    /// <summary>
    /// Handles assigning modifications to a reviewer
    /// </summary>
    /// <param name="request">The command containing the answers to save.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task Handle(AssignModificationsToReviewerCommand request, CancellationToken cancellationToken)
    {
        await projectModificationService.AssignModificationsToReviewer(request.ModificationIds, request.ReviewerId, request.ReviewerEmail, request.ReviewerName);
    }
}