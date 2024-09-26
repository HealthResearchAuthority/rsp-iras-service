using Rsp.QuestionSetService.Application.Contracts.Repositories;
using Rsp.QuestionSetService.Application.Contracts.Services;
using Rsp.QuestionSetService.Domain.Entities;

namespace Rsp.QuestionSetService.Services;

public class ResearchApplicationService(IResearchApplicationRepository researchApplicationRepository) : IResearchApplicationService
{
    public async Task CreateResearchApplicationAsync(string title, string description, string createdBy)
    {
        var researchApplication = new ResearchApplication
        {
            Title = title,
            Description = description,
            UpdatedBy = createdBy
        };

        await researchApplicationRepository.AddAsync(researchApplication);
    }

    public async Task UpdateResearchApplicationStatusAsync(int applicationId, string status, string updatedBy)
    {
        var researchApplication = await researchApplicationRepository.GetByIdAsync(applicationId);

        researchApplication.Status = status;
        researchApplication.UpdatedBy = updatedBy;
        researchApplication.UpdatedDate = DateTime.Now;

        await researchApplicationRepository.UpdateAsync(researchApplication);
    }
}