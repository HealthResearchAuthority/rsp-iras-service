using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

/// <summary>
/// Command to save documents for a modification.
/// </summary>
public class SaveModificationDocumentsCommand(ModificationDocumentDto request) : IRequest
{
    /// <summary>
    /// Gets or sets the modification documents request to be saved.
    /// </summary>
    public ModificationDocumentDto ModificationDocumentRequest { get; set; } = request;
}