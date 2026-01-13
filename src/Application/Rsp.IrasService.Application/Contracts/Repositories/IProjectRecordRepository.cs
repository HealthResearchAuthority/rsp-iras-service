using Ardalis.Specification;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Contracts.Repositories;

public interface IProjectRecordRepository
{
    /// <summary>
    /// Adds a new ProjectRecord to the database
    /// </summary>
    /// <param name="irasApplication">The application values</param>
    Task<ProjectRecord> CreateProjectRecord(ProjectRecord irasApplication);

    /// <summary>
    /// Return a single ProjectRecord from the database
    /// </summary>
    Task<ProjectRecord?> GetProjectRecord(ISpecification<ProjectRecord> specification);

    /// <summary>
    /// Return all or specified number ProjectRecords from the database
    /// </summary>
    Task<IEnumerable<ProjectRecord>> GetProjectRecords(ISpecification<ProjectRecord> specification);

    /// <summary>
    /// Return all or specified number ProjectRecords from the database with pagination
    /// </summary>
    Task<(IEnumerable<ProjectRecord>, int)> GetPaginatedProjectRecords(ISpecification<ProjectRecord> projectsSpecification, ISpecification<ProjectRecordAnswer> projectTitlesSpecification, int pageIndex, int? pageSize, string? sortField, string? sortDirection);

    /// <summary>
    /// Update the values of an ProjectRecord in the database
    /// </summary>
    /// <param name="irasApplication">The ProjectRecord values</param>
    Task<ProjectRecord?> UpdateProjectRecord(ProjectRecord irasApplication);

    /// <summary>
    /// Deletes the project with the specified projectRecordId
    /// </summary>
    /// <param name="specification">The specification to identify the project record to delete</param>
    Task DeleteProjectRecord(GetApplicationSpecification specification);

    Task<(IEnumerable<CompleteProjectRecordResponse>, int)> GetPaginatedProjectRecords
    (
        ProjectRecordSearchRequest request,
        int pageIndex,
        int? pageSize,
        string? sortField,
        string? sortDirection
    );

    Task<IEnumerable<ProjectRecordAuditTrail>> GetProjectRecordAuditTrail(string projectRecordId);

    /// <summary>
    /// Update the status of the project record
    /// </summary>
    /// <param name="specification"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    Task<ProjectRecord?> UpdateProjectRecordStatus(GetApplicationSpecification specification, string status);
}