using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts;

public interface IApplicationRepository
{
    /// <summary>
    /// Adds a new application to the database
    /// </summary>
    /// <param name="irasApplication">The application values</param>
    Task<IrasApplication> CreateApplication(IrasApplication irasApplication);
}