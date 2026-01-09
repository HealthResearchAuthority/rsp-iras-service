using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.Logging.Interceptors;

namespace Rsp.IrasService.Application.Contracts.Services;

/// <summary>
/// Interface to create/read/update the project closure records in the database. Marked as IInterceptable to enable
/// the start/end logging for all methods.
/// </summary>
public interface IProjectClosureService : IInterceptable
{
    /// <summary>
    /// Adds a new project closure record to the database
    /// </summary>
    /// <param name="projectClosureRequest">The project colsure values</param>
    Task<ProjectClosureResponse> CreateProjectClosure(ProjectClosureRequest projectClosureRequest);

    /// <summary>
    /// Returns a single project closure
    /// </summary>
    /// <param name="projectRecordId">Id of the application</param>
    Task<ProjectClosuresSearchResponse> GetProjectClosuresByProjectRecordId(string projectRecordId);

    /// <summary>
    /// Updates the values of an project closure record
    /// </summary>
    /// <param name="projectClosureRequest">The application values</param>
    Task<ProjectClosureResponse> UpdateProjectClosureStatus(ProjectClosureRequest projectClosureRequest);

    /// <summary>
    /// Gets project closures records for specific sponsorOrganisationUserId with filtering, sorting and pagination
    /// </summary>
    /// <param name="sponsorOrganisationUserId">The unique identifier of the sponsor organisation user for which project closures are requested.</param>
    /// <param name="searchQuery">Object containing filtering criteria for project closures.</param>
    /// <param name="pageNumber">The number of the page to retrieve (used for pagination - 1-based).</param>
    /// <param name="pageSize">The number of items per page (used for pagination).</param>
    /// <param name="sortField">The field name by which the results should be sorted.</param>
    /// <param name="sortDirection">The direction of sorting: "asc" for ascending or "desc" for descending.</param>
    /// <returns>Returns a paginated list of project closures.</returns>
    Task<ProjectClosuresSearchResponse> GetProjectClosuresBySponsorOrganisationUserId(Guid sponsorOrganisationUserId, ProjectClosuresSearchRequest searchQuery, int pageNumber, int pageSize, string sortField, string sortDirection);
}