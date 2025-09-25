using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

/// <summary>
/// Command to update a modification document.
/// </summary>
public class UpdateModificationDocumentCommand(ModificationDocumentDto request) : IRequest
{
    /// <summary>
    /// Gets the modification documents request containing the data to update.
    /// </summary>
    public ModificationDocumentDto ModificationDocumentsRequest { get; } = request;
}
