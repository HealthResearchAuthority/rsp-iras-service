using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.Logging.Interceptors;

namespace Rsp.IrasService.Application.Contracts.Services;

/// <summary>
/// Defines operations for creating, retrieving, updating and assigning project modifications
/// and their individual modification changes. Marked as <see cref="IInterceptable"/> so that
/// cross-cutting logging (start/end, exceptions) can be applied automatically.
/// </summary>
public interface IProjectModificationService : IInterceptable
{
    /// <summary>
    /// Creates a new project modification.
    /// </summary>
    /// <param name="modificationRequest">The request payload containing the modification details to persist.</param>
    /// <returns>A <see cref="ModificationResponse"/> describing the created modification.</returns>
    Task<ModificationResponse> CreateModification(ModificationRequest modificationRequest);

    /// <summary>
    /// Creates a new modification change or updates an existing one (upsert semantics).
    /// </summary>
    /// <param name="modificationRequest">The change details (the presence of an Id determines update vs create).</param>
    /// <returns>A <see cref="ModificationChangeResponse"/> representing the stored modification change.</returns>
    Task<ModificationChangeResponse> CreateOrUpdateModificationChange(ModificationChangeRequest modificationRequest);

    /// <summary>
    /// Retrieves a single modification change by its unique identifier.
    /// </summary>
    /// <param name="modificationChangeId">The unique identifier of the modification change to retrieve.</param>
    /// <returns>A <see cref="ModificationChangeResponse"/> if found; otherwise typically a not-found result should be surfaced by the implementation.</returns>
    Task<ModificationChangeResponse> GetModificationChange(Guid modificationChangeId);

    /// <summary>
    /// Retrieves a all modification changes by for the modification id.
    /// </summary>
    /// <param name="projectModificationId">The unique identifier of the modification.</param>
    /// <returns>A <see cref="IEnumerable<ModificationChangeResponse>"/> if found; otherwise typically a not-found result should be surfaced by the implementation.</returns>
    Task<IEnumerable<ModificationChangeResponse>> GetModificationChanges(Guid projectModificationId);

    /// <summary>
    /// Retrieves a paginated, optionally filtered and sorted list of modifications across all projects.
    /// </summary>
    /// <param name="searchQuery">Filter criteria (may be null properties when no filtering is desired).</param>
    /// <param name="pageNumber">1-based page index.</param>
    /// <param name="pageSize">Maximum number of records per page.</param>
    /// <param name="sortField">The field name to sort by (implementation defines allowable values).</param>
    /// <param name="sortDirection">The sort direction: typically 'asc' or 'desc' (case-insensitive).</param>
    /// <returns>A <see cref="ModificationResponse"/> containing the page of modifications plus total count metadata.</returns>
    Task<ModificationResponse> GetModifications
    (
        ModificationSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection
    );

    /// <summary>
    /// Retrieves a paginated, optionally filtered and sorted list of modifications for a single project.
    /// </summary>
    /// <param name="projectRecordId">The unique record identifier for the project whose modifications are requested.</param>
    /// <param name="searchQuery">Additional filter criteria scoped to this project.</param>
    /// <param name="pageNumber">1-based page index.</param>
    /// <param name="pageSize">Maximum number of records per page.</param>
    /// <param name="sortField">The field name to sort by (implementation defines allowable values).</param>
    /// <param name="sortDirection">The sort direction: typically 'asc' or 'desc'.</param>
    /// <returns>A <see cref="ModificationResponse"/> containing the filtered page of modifications for the project.</returns>
    Task<ModificationResponse> GetModificationsForProject
    (
       string projectRecordId,
       ModificationSearchRequest searchQuery,
       int pageNumber,
       int pageSize,
       string sortField,
       string sortDirection
    );

    /// <summary>
    /// Retrieves modifications for the specified collection of modification identifiers.
    /// </summary>
    /// <param name="Ids">A list of modification identifiers (string form) to fetch.</param>
    /// <returns>A <see cref="ModificationResponse"/> containing the matching modifications.</returns>
    Task<ModificationResponse> GetModificationsByIds(List<string> Ids);

    /// <summary>
    /// Assigns the specified modifications to the given reviewer.
    /// </summary>
    /// <param name="modificationIds">The list of modification identifiers to assign.</param>
    /// <param name="reviewerId">The identifier of the reviewer receiving the assignment.</param>
    /// <returns>A task representing the asynchronous assignment operation.</returns>
    Task AssignModificationsToReviewer(List<string> modificationIds, string reviewerId);

    Task<ProjectOverviewDocumentResponse> GetDocumentsForProjectOverview(
       string projectRecordId,
       ProjectOverviewDocumentSearchRequest searchQuery,
       int pageNumber,
       int pageSize,
       string sortField,
       string sortDirection);

    /// <summary>
    /// Removes an existing modification change by its unique identifier.
    /// </summary>
    /// <param name="modificationChangeId">The unique identifier of the modification change to remove.</param>
    /// <returns>A task representing the asynchronous remove operation.</returns>
    Task RemoveModificationChange(Guid modificationChangeId);

    /// <summary>
    /// Updates an existing modification status by its unique identifier. And also updates
    /// the status of the associated modification changes.
    /// </summary>
    /// <param name="modificationId">The unique identifier of the modification change to remove.</param>
    Task UpdateModificationStatus(Guid modificationId, string status);
}