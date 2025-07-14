using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

/// <summary>
/// Command to save participating organisations for a modification.
/// </summary>
public class SaveModificationParticipatingOrganisationsCommand(ModificationParticipatingOrganisationDto request) : IRequest
{
    /// <summary>
    /// Gets or sets the modification participating organisations request containing the organisations to be saved.
    /// </summary>
    public ModificationParticipatingOrganisationDto ModificationParticipatingOrganisationRequest { get; set; } = request;
}