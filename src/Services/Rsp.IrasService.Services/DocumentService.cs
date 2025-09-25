using Mapster;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

/// <summary>
///     Service for managing modification documents for project records.
/// </summary>
public class DocumentService(IDocumentRepository documentRepository) : IDocumentService
{
    public Task UpdateModificationDocument(ModificationDocumentDto modificationDocumentDto)
    {
        // Map the request DTO to the domain entity
        var modificationDocument = modificationDocumentDto.Adapt<ModificationDocument>();

        return documentRepository.UpdateModificationDocument(modificationDocument);
    }
}