using Mapster;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

/// <summary>
/// Service for managing project modifications and their changes.
/// </summary>
public class ProjectModificationService(IProjectModificationRepository projectModificationRepository) : IProjectModificationService
{
    /// <summary>
    /// Adds a new project modification to the database.
    /// </summary>
    /// <param name="modificationRequest">The modification request containing application values.</param>
    /// <returns>A <see cref="ModificationResponse"/> containing details of the created modification.</returns>
    public async Task<ModificationResponse> CreateModification(ModificationRequest modificationRequest)
    {
        // Map the request DTO to the domain entity
        var projectModification = modificationRequest.Adapt<ProjectModification>();

        // Persist the new modification entity
        var createdModification = await projectModificationRepository.CreateModification(projectModification);

        // Map the created entity to the response DTO
        return createdModification.Adapt<ModificationResponse>();
    }

    /// <summary>
    /// Creates or updates a modification change in the database.
    /// </summary>
    /// <param name="modificationRequest">The modification change request containing change details.</param>
    /// <returns>A <see cref="ModificationChangeResponse"/> containing details of the created or updated modification change.</returns>
    public async Task<ModificationChangeResponse> CreateOrUpdateModificationChange(ModificationChangeRequest modificationRequest)
    {
        // Map the request DTO to the domain entity
        var projectModificationChange = modificationRequest.Adapt<ProjectModificationChange>();

        // Persist the modification change entity
        var createdModificationChange = await projectModificationRepository.CreateModificationChange(projectModificationChange);

        // Map the created entity to the response DTO
        return createdModificationChange.Adapt<ModificationChangeResponse>();
    }
}