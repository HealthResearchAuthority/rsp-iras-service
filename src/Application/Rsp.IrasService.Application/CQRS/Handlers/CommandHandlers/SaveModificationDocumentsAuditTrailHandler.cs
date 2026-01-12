using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

/// <summary>
/// Handler for saving modification documents audit trail.
/// </summary>
public class SaveModificationDocumentsAuditTrailHandler(IRespondentService respondentService) : IRequestHandler<SaveModificationDocumentsAuditTrailCommand>
{
    /// <summary>
    /// Handles saving the modification documents audit trail.
    /// </summary>
    /// <param name="request">The command containing the documents audit trail to save.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task Handle(SaveModificationDocumentsAuditTrailCommand request, CancellationToken cancellationToken)
    {
        await respondentService.SaveModificationDocumentsAuditTrail(request.DocumentsAuditTrailRequest);
    }
}