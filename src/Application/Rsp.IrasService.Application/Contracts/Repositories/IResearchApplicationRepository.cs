using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IResearchApplicationRepository
{
    Task<ProjectApplication> GetByIdAsync(int applicationId);

    Task AddAsync(ProjectApplication researchApplication);

    Task UpdateAsync(ProjectApplication researchApplication);
}