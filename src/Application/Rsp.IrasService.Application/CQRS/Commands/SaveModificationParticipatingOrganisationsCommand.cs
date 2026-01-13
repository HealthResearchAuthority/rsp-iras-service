using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

/// <summary>
/// Command to save participating organisations for a modification.
/// </summary>
public class SaveModificationParticipatingOrganisationsCommand(List<ModificationParticipatingOrganisationDto> request) : IRequest
{
    /// <summary>
    /// Gets or sets the modification participating organisations request containing the organisations to be saved.
    /// </summary>
    public List<ModificationParticipatingOrganisationDto> ModificationParticipatingOrganisationRequest { get; set; } = request;
}