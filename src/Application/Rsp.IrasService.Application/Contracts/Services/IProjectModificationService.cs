using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.Logging.Interceptors;

namespace Rsp.IrasService.Application.Contracts.Services;

/// <summary>
/// Interface to create, read, and update modification records in the database.
/// Marked as IInterceptable to enable start/end logging for all methods.
/// </summary>
public interface IProjectModificationService : IInterceptable
{
    /// <summary>
    /// Adds a new project modification to the database.
    /// </summary>
    /// <param name="modificationRequest">The modification request containing application values.</param>
    /// <returns>A <see cref="ModificationResponse"/> containing details of the created modification.</returns>
    Task<ModificationResponse> CreateModification(ModificationRequest modificationRequest);

    /// <summary>
    /// Creates or updates a modification change in the database.
    /// </summary>
    /// <param name="modificationRequest">The modification change request containing change details.</param>
    /// <returns>A <see cref="ModificationChangeResponse"/> containing details of the created or updated modification change.</returns>
    Task<ModificationChangeResponse> CreateOrUpdateModificationChange(ModificationChangeRequest modificationRequest);

    Task<ModificationResponse> GetModifications(
        ModificationSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection);

    Task<ModificationResponse> GetModificationsForProject(
       string projectRecordId,
       ModificationSearchRequest searchQuery,
       int pageNumber,
       int pageSize,
       string sortField,
       string sortDirection);

    Task<ModificationResponse> GetModificationsByIds(List<string> Ids);

    Task AssignModificationsToReviewer(List<string> modificationIds, string reviewerId);

    Task<ProjectOverviewDocumentResponse> GetDocumentsForProjectOverview(
       string projectRecordId,
       ProjectOverviewDocumentSearchRequest searchQuery,
       int pageNumber,
       int pageSize,
       string sortField,
       string sortDirection);
}