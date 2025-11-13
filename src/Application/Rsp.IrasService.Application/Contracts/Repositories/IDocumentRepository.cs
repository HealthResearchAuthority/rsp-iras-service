using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

/// <summary>
///     Repository interface for managing modification documents
/// </summary>
public interface IDocumentRepository
{
    Task<int?> UpdateModificationDocumentStatus(ModificationDocument modificationDocument);
}