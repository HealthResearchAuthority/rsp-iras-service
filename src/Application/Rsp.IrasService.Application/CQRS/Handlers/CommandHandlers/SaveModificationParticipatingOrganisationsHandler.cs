using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

/// <summary>
/// Handler for saving modification answers.
/// </summary>
public class SaveModificationParticipatingOrganisationsHandler(IRespondentService respondentService) : IRequestHandler<SaveModificationParticipatingOrganisationsCommand>
{
    /// <summary>
    /// Handles saving the modification answers.
    /// </summary>
    /// <param name="request">The command containing the answers to save.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task Handle(SaveModificationParticipatingOrganisationsCommand request, CancellationToken cancellationToken)
    {
        await respondentService.SaveModificationParticipatingOrganisationResponses(request.ModificationParticipatingOrganisationRequest);
    }
}