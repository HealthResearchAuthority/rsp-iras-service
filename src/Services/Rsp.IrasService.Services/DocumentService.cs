using Mapster;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Services;

/// <summary>
///     Service for managing modification documents for project records.
/// </summary>
public class DocumentService(IDocumentRepository documentRepository) : IDocumentService
{
    public Task<int?> UpdateModificationDocument(ModificationDocumentDto modificationDocumentDto)
    {
        // Map the request DTO to the domain entity
        var modificationDocument = modificationDocumentDto.Adapt<ModificationDocument>();

        return documentRepository.UpdateModificationDocumentStatus(modificationDocument);
    }
}