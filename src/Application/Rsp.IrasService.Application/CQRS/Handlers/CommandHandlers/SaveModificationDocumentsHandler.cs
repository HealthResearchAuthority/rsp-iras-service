using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

/// <summary>
/// Handler for saving modification documents audit trail.
/// </summary>
public class SaveModificationDocumentsHandler(IRespondentService respondentService) : IRequestHandler<SaveModificationDocumentsCommand>
{
    /// <summary>
    /// Handles saving the modification documents audit trail.
    /// </summary>
    /// <param name="request">The command containing the documents audit trail to save.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task Handle(SaveModificationDocumentsCommand request, CancellationToken cancellationToken)
    {
        await respondentService.SaveModificationDocumentResponses(request.ModificationDocumentsRequest);
    }
}