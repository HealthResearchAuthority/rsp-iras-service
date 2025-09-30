using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

/// <summary>
/// Handler for updating modification documents.
/// </summary>
public class UpdateModificationDocumentHandler(IDocumentService documentService)
    : IRequestHandler<UpdateModificationDocumentCommand, int?>
{
    public async Task<int?> Handle(UpdateModificationDocumentCommand request, CancellationToken cancellationToken)
    {
        return await documentService.UpdateModificationDocument(request.ModificationDocumentsRequest);
    }
}
