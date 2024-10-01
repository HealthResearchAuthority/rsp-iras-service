using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

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