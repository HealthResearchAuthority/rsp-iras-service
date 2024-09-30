using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts;

public interface IResearchApplicationRepository
{
    Task<ResearchApplication> GetByIdAsync(int applicationId);

    Task AddAsync(ResearchApplication researchApplication);

    Task UpdateAsync(ResearchApplication researchApplication);
}