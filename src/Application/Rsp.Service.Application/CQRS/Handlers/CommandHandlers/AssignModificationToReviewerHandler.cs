using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

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