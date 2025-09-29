using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

/// <summary>
/// Command to delete modification documents for a project.
/// </summary>
public class DeleteModificationDocumentsCommand(List<ModificationDocumentDto> request) : IRequest
{
    /// <summary>
    /// Gets or sets the modification documents request containing the answers to be deleted.
    /// </summary>
    public List<ModificationDocumentDto> ModificationDocumentsRequest { get; set; } = request;
}