using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Contracts.Repositories;

/// <summary>
///     Repository interface for managing modification documents
/// </summary>
public interface IDocumentRepository
{
    Task<int?> UpdateModificationDocumentStatus(ModificationDocument modificationDocument);
}