using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

/// <summary>
/// Handler for saving modification answers.
/// </summary>
public class DeleteModificationDocumentsHandler(IRespondentService respondentService) : IRequestHandler<DeleteModificationDocumentsCommand>
{
    /// <summary>
    /// Handles saving the modification answers.
    /// </summary>
    /// <param name="request">The command containing the answers to save.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task Handle(DeleteModificationDocumentsCommand request, CancellationToken cancellationToken)
    {
        await respondentService.DeleteModificationDocumentResponses(request.ModificationDocumentsRequest);
    }
}