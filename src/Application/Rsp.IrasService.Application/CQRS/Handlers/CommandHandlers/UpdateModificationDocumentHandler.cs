using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

/// <summary>
/// Handler for updating modification documents.
/// </summary>
public class UpdateModificationDocumentHandler(IDocumentService documentService) : IRequestHandler<UpdateModificationDocumentCommand>
{
    public async Task Handle(UpdateModificationDocumentCommand request, CancellationToken cancellationToken)
    {
        await documentService.UpdateModificationDocument(request.ModificationDocumentsRequest);
    }
}