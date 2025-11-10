using Mapster;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Specifications;
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

    public async Task<ModificationChangeResponse> GetModificationChange(Guid modificationChangeId)
    {
        var specification = new GetModificationChangeSpecification(modificationChangeId);

        // Get the new modification change
        var modificationChange = await projectModificationRepository.GetModificationChange(specification);

        return modificationChange.Adapt<ModificationChangeResponse>();
    }

    public async Task<IEnumerable<ModificationChangeResponse>> GetModificationChanges(Guid projectModificationId)
    {
        var specification = new GetModificationChangesSpecification(projectModificationId);

        var modificationChanges = await projectModificationRepository.GetModificationChanges(specification);

        return modificationChanges.Adapt<IEnumerable<ModificationChangeResponse>>();
    }

    public Task<ModificationResponse> GetModifications
    (
        ModificationSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection
    )
    {
        var modifications = projectModificationRepository.GetModifications(searchQuery, pageNumber, pageSize, sortField, sortDirection);
        var totalCount = projectModificationRepository.GetModificationsCount(searchQuery);

        return Task.FromResult(new ModificationResponse
        {
            Modifications = modifications.Adapt<IEnumerable<ModificationDto>>(),
            TotalCount = totalCount
        });
    }

    public Task<ModificationResponse> GetModificationsForProject
    (
        string projectRecordId,
        ModificationSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection
    )
    {
        var modifications = projectModificationRepository.GetModifications(searchQuery, pageNumber, pageSize, sortField, sortDirection, projectRecordId);
        var totalCount = projectModificationRepository.GetModificationsCount(searchQuery, projectRecordId);

        return Task.FromResult(new ModificationResponse
        {
            Modifications = modifications.Adapt<IEnumerable<ModificationDto>>(),
            TotalCount = totalCount,
            ProjectRecordId = projectRecordId
        });
    }

    public Task<ModificationResponse> GetModificationsByIds(List<string> Ids)
    {
        var modifications = projectModificationRepository.GetModificationsByIds(Ids);

        return Task.FromResult(new ModificationResponse
        {
            Modifications = modifications.Adapt<IEnumerable<ModificationDto>>(),
        });
    }

    public Task AssignModificationsToReviewer(List<string> modificationIds, string reviewerId, string reviewerEmail, string reviewerName)
    {
        return projectModificationRepository.AssignModificationsToReviewer(modificationIds, reviewerId, reviewerEmail, reviewerName);
    }

    public Task<ProjectOverviewDocumentResponse> GetDocumentsForProjectOverview(
        string projectRecordId,
        ProjectOverviewDocumentSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection)
    {
        var modifications = projectModificationRepository.GetDocumentsForProjectOverview(searchQuery, pageNumber, pageSize, sortField, sortDirection, projectRecordId);
        var totalCount = projectModificationRepository.GetDocumentsForProjectOverviewCount(searchQuery, projectRecordId);

        return Task.FromResult(new ProjectOverviewDocumentResponse
        {
            Documents = modifications.Adapt<IEnumerable<ProjectOverviewDocumentDto>>(),
            TotalCount = totalCount,
            ProjectRecordId = projectRecordId
        });
    }

    /// <summary>
    /// Removes an existing modification change by its unique identifier.
    /// </summary>
    /// <param name="modificationChangeId">The unique identifier of the modification change to remove.</param>
    public async Task RemoveModificationChange(Guid modificationChangeId)
    {
        var specification = new GetModificationChangeSpecification(modificationChangeId);

        await projectModificationRepository.RemoveModificationChange(specification);
    }

    /// <summary>
    /// Updates an existing modification status by its unique identifier. And also updates
    /// the status of the associated modification changes.
    /// </summary>
    /// <param name="modificationId">The unique identifier of the modification change to remove.</param>
    public async Task UpdateModificationStatus(Guid modificationId, string status)
    {
        var specification = new GetModificationSpecification(modificationId);

        await projectModificationRepository.UpdateModificationStatus(specification, status);
    }

    /// <summary>
    /// Updates an existing modification status by its unique identifier. And also updates
    /// the status of the associated modification changes.
    /// </summary>
    /// <param name="modificationId">The unique identifier of the modification change to remove.</param>
    public async Task DeleteModification(Guid modificationId)
    {
        var specification = new GetModificationSpecification(modificationId);

        await projectModificationRepository.DeleteModification(specification);
    }

    public async Task<ModificationAuditTrailResponse> GetModificationAuditTrail(Guid projectModificationId)
    {
        var auditTrailEntries = await projectModificationRepository.GetModificationAuditTrail(projectModificationId);

        return new ModificationAuditTrailResponse
        {
            Items = auditTrailEntries.Adapt<IEnumerable<ModificationAuditTrailDto>>(),
            TotalCount = auditTrailEntries.Count()
        };
    }

    public Task<ModificationResponse> GetModificationsBySponsorOrganisationUserId
    (
        Guid sponsorOrganisationUserId,
        SponsorAuthorisationsSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection
    )
    {
        var modifications = projectModificationRepository.GetModificationsBySponsorOrganisationUser(searchQuery, pageNumber, pageSize, sortField, sortDirection, sponsorOrganisationUserId);
        var totalCount = projectModificationRepository.GetModificationsBySponsorOrganisationUserCount(searchQuery, sponsorOrganisationUserId);

        return Task.FromResult(new ModificationResponse
        {
            Modifications = modifications.Adapt<IEnumerable<ModificationDto>>(),
            TotalCount = totalCount
        });
    }
}