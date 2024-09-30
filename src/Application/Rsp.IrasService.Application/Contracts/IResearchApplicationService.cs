namespace Rsp.IrasService.Application.Contracts;

public interface IResearchApplicationService
{
    Task CreateResearchApplicationAsync(string title, string description, string createdBy);

    Task UpdateResearchApplicationStatusAsync(int applicationId, string status, string updatedBy);
}