using Rsp.QuestionSetService.Domain.Entities;

namespace Rsp.QuestionSetService.Application.Contracts.Repositories;

public interface IResearchApplicationRepository
{
    Task<ResearchApplication> GetByIdAsync(int applicationId);

    Task AddAsync(ResearchApplication researchApplication);

    Task UpdateAsync(ResearchApplication researchApplication);
}