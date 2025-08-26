using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

/// <summary>
/// Handler for saving modification answers.
/// </summary>
public class SaveModificationDocumentAnswerHandler(IRespondentService respondentService) : IRequestHandler<SaveModificationDocumentAnswersCommand>
{
    /// <summary>
    /// Handles saving the modification answers.
    /// </summary>
    /// <param name="request">The command containing the answers to save.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task Handle(SaveModificationDocumentAnswersCommand request, CancellationToken cancellationToken)
    {
        await respondentService.SaveModificationDocumentAnswerResponses(request.ModificationDocumentAnswerRequest);
    }
}