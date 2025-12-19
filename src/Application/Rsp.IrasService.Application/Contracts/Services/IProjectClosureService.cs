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
    Task<ProjectClosureResponse> GetProjectClosure(string projectRecordId);

    /// <summary>
    /// Updates the values of an project closure record
    /// </summary>
    /// <param name="projectClosureRequest">The application values</param>
    Task<ProjectClosureResponse> UpdateProjectClosureStatus(ProjectClosureRequest projectClosureRequest);
}