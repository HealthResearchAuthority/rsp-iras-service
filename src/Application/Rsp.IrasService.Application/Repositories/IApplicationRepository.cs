using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Repositories;

public interface IApplicationRepository
{
    /// <summary>
    /// Adds a new application to the database
    /// </summary>
    /// <param name="irasApplication">The application values</param>
    Task<IrasApplication> CreateApplication(IrasApplication irasApplication);

    Task<IrasApplication> GetApplication(int applicationId);

    Task<IEnumerable<IrasApplication>> GetApplications();

    Task<IrasApplication> UpdateApplication(int applicationId, IrasApplication irasApplication);
}