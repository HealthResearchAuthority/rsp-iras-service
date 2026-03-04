using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Contracts.Repositories;

/// <summary>
///     Repository interface for managing modification documents
/// </summary>
public interface IDocumentRepository
{
    Task<int?> UpdateModificationDocumentStatus(ModificationDocument modificationDocument);

    Task<IEnumerable<ModificationDocumentsAuditTrailDto>> GetProjectDocumentsAuditTrail
    (
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        string projectRecordId,
        bool includeTotalCount = false
    );
}