using Rsp.Logging.Interceptors;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.Contracts.Services;

/// <summary>
///     Interface to create, read, and update document records in the database.
///     Marked as IInterceptable to enable start/end logging for all methods.
/// </summary>
public interface IDocumentService : IInterceptable
{
    Task<int?> UpdateModificationDocument(ModificationDocumentDto modificationDocumentDto);

    /// <summary>
    /// Retrieves the audit trail for a specific project documents.
    /// </summary>
    /// <param name="projectRecordId">The unique identifier of the project</param>
    /// <returns>A list of documents audit trail records and the total count</returns>
    Task<ProjectDocumentsAuditTrailResponse> GetProjectDocumentsAuditTrail
    (
       string projectRecordId,
       int pageNumber,
       int pageSize,
       string sortField,
       string sortDirection
    );
}