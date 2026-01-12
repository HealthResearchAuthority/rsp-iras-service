using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

/// <summary>
/// Handler for saving modification answers.
/// </summary>
public class SaveModificationParticipatingOrganisationAnswerHandler(IRespondentService respondentService) : IRequestHandler<SaveModificationParticipatingOrganisationAnswersCommand>
{
    /// <summary>
    /// Handles saving the modification answers.
    /// </summary>
    /// <param name="request">The command containing the answers to save.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task Handle(SaveModificationParticipatingOrganisationAnswersCommand request, CancellationToken cancellationToken)
    {
        await respondentService.SaveModificationParticipatingOrganisationAnswerResponses(request.ModificationParticipatingOrganisationAnswerRequest);
    }
}