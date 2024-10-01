using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IResearchApplicationRepository
{
    Task<ResearchApplication> GetByIdAsync(int applicationId);

    Task AddAsync(ResearchApplication researchApplication);

    Task UpdateAsync(ResearchApplication researchApplication);
}