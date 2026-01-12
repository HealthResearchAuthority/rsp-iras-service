using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

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
