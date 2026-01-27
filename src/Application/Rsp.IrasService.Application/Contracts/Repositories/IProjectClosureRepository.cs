using Ardalis.Specification;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Contracts.Repositories;

public interface IProjectClosureRepository
{
    /// <summary>
    /// Add new project closure record to database
    /// </summary>
    /// <param name="projectClosure"></param>
    /// <returns></returns>
    Task<ProjectClosure> CreateProjectClosure(ProjectClosure projectClosure);

    /// <summary>
    /// Update the projectClosure records (and projectRecord status to closed for Authorised project closure status)
    /// </summary>
    /// <param name="specification"></param>
    /// <param name="status"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<ProjectClosure?> UpdateProjectClosureStatus(ISpecification<ProjectClosure> specification, string status, string userId);

    /// <summary>
    /// Returns the singale projcect closure record from database
    /// </summary>
    /// <param name="projectRecordId"></param>
    /// <returns></returns>
    Task<IEnumerable<ProjectClosure>> GetProjectClosures(string projectRecordId);

    /// <summary>
    /// Retrieves a paged, sorted list of sponsor organisation user project closures matching the supplied search criteria.
    /// </summary>
    /// <param name="searchQuery">Object containing filtering criteria for project closures.</param>
    /// <param name="pageNumber">1-based page number.</param>
    /// <param name="pageSize">Number of records per page. Implementations should guard against excessive values.</param>
    /// <param name="sortField">Property / column name to sort by (case-insensitive). Implementations should validate against a whitelist.</param>
    /// <param name="sortDirection">Sort direction: typically "ASC" or "DESC" (case-insensitive).</param>
    /// <param name="sponsorOrganisationUserId">The unique identifier of the sponsor organisation user for which project closures are requested</param>
    /// <returns>Paged sequence of <see cref="ProjectClosure"/> projections.</returns>
    /// <remarks>
    /// Expected to perform server-side filtering, ordering and paging for efficiency.
    /// If <paramref name="pageNumber"/> or <paramref name="pageSize"/> are invalid, implementations may normalize them.
    /// </remarks>
    IEnumerable<ProjectClosure> GetProjectClosuresBySponsorOrganisationUser
    (
       ProjectClosuresSearchRequest searchQuery,
       int pageNumber,
       int pageSize,
       string sortField,
       string sortDirection,
       Guid sponsorOrganisationUserId
    );

    /// <summary>
    /// Returns the total count of modifications matching the supplied search criteria (ignoring paging).
    /// </summary>
    /// <param name="searchQuery">Same filtering contract as <see cref="GetProjectClosuresBySponsorOrganisationUser"/>.</param>
    /// <param name="sponsorOrganisationUserId">Sponsor organisation user unique identifier.</param>
    /// <returns>Total number of matching records.</returns>
    int GetProjectClosuresBySponsorOrganisationUserCount(ProjectClosuresSearchRequest searchQuery, Guid sponsorOrganisationUserId);

    /// <summary>
    /// Retrieves a collection of sponsor organisation user project closures matching the supplied search criteria.
    /// </summary>
    /// <param name="searchQuery">Object containing filtering criteria for project closures.</param>
    /// <param name="sponsorOrganisationUserId">The unique identifier of the sponsor organisation user for which project closures are requested</param>
    /// <returns>Sequence of <see cref="ProjectClosure"/> projections.</returns>
    /// <remarks>
    /// Expected to perform server-side filtering for efficiency, but no paging.
    /// </remarks>
    public IEnumerable<ProjectClosure> GetProjectClosuresBySponsorOrganisationUserWithoutPaging
    (
       ProjectClosuresSearchRequest searchQuery,
       Guid sponsorOrganisationUserId
    );
}