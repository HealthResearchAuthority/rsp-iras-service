using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

/// <summary>
/// Command to save participating organisation answers for a modification.
/// </summary>
public class SaveModificationParticipatingOrganisationAnswersCommand(ModificationParticipatingOrganisationAnswerDto request) : IRequest
{
    /// <summary>
    /// Gets or sets the modification participating organisation answers request containing the organisations answers to be saved.
    /// </summary>
    public ModificationParticipatingOrganisationAnswerDto ModificationParticipatingOrganisationAnswerRequest { get; set; } = request;
}