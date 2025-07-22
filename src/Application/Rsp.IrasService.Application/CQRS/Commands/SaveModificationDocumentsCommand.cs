using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

/// <summary>
/// Command to save modification answers for a project.
/// </summary>
public class SaveModificationDocumentsCommand(List<ModificationDocumentDto> request) : IRequest
{
    /// <summary>
    /// Gets or sets the modification documents request containing the answers to be saved.
    /// </summary>
    public List<ModificationDocumentDto> ModificationDocumentsRequest { get; set; } = request;
}