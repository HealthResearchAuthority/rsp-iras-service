using Mapster;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

public class ProjectModificationService(IProjectModificationRepository applicationRepository) : IProjectModificationService
{
    public async Task<ModificationResponse> CreateModification(ModificationRequest modificationRequest)
    {
        var projectModification = modificationRequest.Adapt<ProjectModification>();

        var createdModification = await applicationRepository.CreateModification(projectModification);

        return createdModification.Adapt<ModificationResponse>();
    }

    public async Task<ModificationChangeResponse> CreateOrUpdateModificationChange(ModificationChangeRequest modificationRequest)
    {
        var projectModificationChange = modificationRequest.Adapt<ProjectModificationChange>();

        var createdModificationChange = await applicationRepository.CreateModificationChange(projectModificationChange);

        return createdModificationChange.Adapt<ModificationChangeResponse>();
    }
}