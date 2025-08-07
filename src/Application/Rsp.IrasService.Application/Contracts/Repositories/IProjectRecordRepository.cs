using Ardalis.Specification;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IProjectRecordRepository
{
    /// <summary>
    /// Adds a new ProjectRecord to the database
    /// </summary>
    /// <param name="irasApplication">The application values</param>
    Task<ProjectRecord> CreateProjectRecord(ProjectRecord irasApplication, ProjectPersonnel respondent);

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
    Task<(IEnumerable<ProjectRecord>, int)> GetPaginatedProjectRecords(ISpecification<ProjectRecord> specification, int pageIndex, int? pageSize);

    /// <summary>
    /// Update the values of an ProjectRecord in the database
    /// </summary>
    /// <param name="irasApplication">The ProjectRecord values</param>
    Task<ProjectRecord?> UpdateProjectRecord(ProjectRecord irasApplication);

    IEnumerable<ProjectModificationResult> GetModifications
    (
        ModificationSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection
    );

    int GetModificationsCount(ModificationSearchRequest searchQuery);
}