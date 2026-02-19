using Mapster;
using Microsoft.AspNetCore.Http;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Constants;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Services;

/// <summary>
/// Service for managing project modifications and their changes.
/// </summary>
public class ProjectModificationService(IProjectModificationRepository projectModificationRepository, IHttpContextAccessor httpContextAccessor) : IProjectModificationService
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
    /// <returns>
    /// A <see cref="ModificationChangeResponse"/> containing details of the created or updated
    /// modification change.
    /// </returns>
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

    public async Task<IEnumerable<ModificationChangeResponse>> GetModificationChanges(string projectRecordId, Guid projectModificationId)
    {
        var specification = new GetModificationChangesSpecification(projectRecordId, projectModificationId);

        var modificationChanges = await projectModificationRepository.GetModificationChanges(specification);

        return modificationChanges.Adapt<IEnumerable<ModificationChangeResponse>>();
    }

    public Task<ModificationSearchResponse> GetModifications
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

        return Task.FromResult(new ModificationSearchResponse
        {
            Modifications = modifications.Adapt<IEnumerable<ModificationDto>>(),
            TotalCount = totalCount
        });
    }

    public Task<ModificationSearchResponse> GetModificationsForProject
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

        return Task.FromResult(new ModificationSearchResponse
        {
            Modifications = modifications.Adapt<IEnumerable<ModificationDto>>(),
            TotalCount = totalCount,
            ProjectRecordId = projectRecordId
        });
    }

    public Task<ModificationSearchResponse> GetModificationsByIds(List<string> Ids)
    {
        var modifications = projectModificationRepository.GetModificationsByIds(Ids);

        return Task.FromResult(new ModificationSearchResponse
        {
            Modifications = modifications.Adapt<IEnumerable<ModificationDto>>(),
        });
    }

    public Task AssignModificationsToReviewer(List<string> modificationIds, string reviewerId, string reviewerEmail, string reviewerName)
    {
        return projectModificationRepository.AssignModificationsToReviewer(modificationIds, reviewerId, reviewerEmail, reviewerName);
    }

    public Task<ProjectOverviewDocumentResponse> GetDocumentsForProjectOverview
    (
        string projectRecordId,
        ProjectOverviewDocumentSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection
    )
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
    /// <param name="modificationChangeId">
    /// The unique identifier of the modification change to remove.
    /// </param>
    public async Task RemoveModificationChange(Guid modificationChangeId)
    {
        var specification = new GetModificationChangeSpecification(modificationChangeId);

        await projectModificationRepository.RemoveModificationChange(specification);
    }

    /// <summary>
    /// Updates an existing modification status by its unique identifier. And also updates the
    /// status of the associated modification changes.
    /// </summary>
    /// <param name="modificationChangeRequest">
    /// The request containing the updated modification change details.
    /// </param>
    public async Task UpdateModificationChange(UpdateModificationChangeRequest modificationChangeRequest)
    {
        var specification = new GetModificationChangeSpecification(modificationChangeRequest.Id);

        var modificationChange = modificationChangeRequest.Adapt<ProjectModificationChange>();

        await projectModificationRepository.UpdateModificationChange(specification, modificationChange);
    }

    /// <summary>
    /// Updates an existing modification status by its unique identifier. And also updates the
    /// status of the associated modification changes.
    /// </summary>
    /// <param name="modificationId">The unique identifier of the modification change to remove.</param>
    public async Task UpdateModificationStatus(string projectRecordId, Guid modificationId, string status, string? revisionDescription, string? reasonNotApproved, string? applicantRevisionResponse)
    {
        var specification = new GetModificationSpecification(projectRecordId, modificationId);

        await projectModificationRepository.UpdateModificationStatus(specification, status, revisionDescription, reasonNotApproved, applicantRevisionResponse);
    }

    /// <summary>
    /// Updates an existing modification status by its unique identifier. And also updates the
    /// status of the associated modification changes.
    /// </summary>
    /// <param name="modificationRequest">The</param>
    public async Task UpdateModification(UpdateModificationRequest modificationRequest)
    {
        var specification = new GetModificationSpecification(modificationRequest.ProjectRecordId, modificationRequest.Id);

        var projectModification = modificationRequest.Adapt<ProjectModification>();

        await projectModificationRepository.UpdateModification(specification, projectModification);
    }

    /// <summary>
    /// Updates an existing modification status by its unique identifier. And also updates the
    /// status of the associated modification changes.
    /// </summary>
    /// <param name="modificationId">The unique identifier of the modification change to remove.</param>
    public async Task DeleteModification(string projectRecordId, Guid modificationId)
    {
        var specification = new GetModificationSpecification(projectRecordId, modificationId);

        await projectModificationRepository.DeleteModification(specification);
    }

    public async Task<ModificationAuditTrailResponse> GetModificationAuditTrail(Guid projectModificationId)
    {
        var auditTrailEntries = await projectModificationRepository.GetModificationAuditTrail(projectModificationId);

        var user = httpContextAccessor.HttpContext?.User!;

        if (!(user.IsInRole(Roles.TeamManager) ||
            user.IsInRole(Roles.WorkflowCoordinator) ||
            user.IsInRole(Roles.StudyWideReviewer) ||
            user.IsInRole(Roles.SystemAdministrator)))
        {
            auditTrailEntries = auditTrailEntries
                .Where(entry => !entry.IsBackstageOnly)
                .Select(entry =>
                {
                    entry.User = entry.ShowUserEmailToFrontstage ? entry.User : "";
                    return entry;
                });
        }

        return new ModificationAuditTrailResponse
        {
            Items = auditTrailEntries.Adapt<IEnumerable<ModificationAuditTrailDto>>(),
            TotalCount = auditTrailEntries.Count()
        };
    }

    public Task<ModificationSearchResponse> GetModificationsBySponsorOrganisationUserId
    (
        Guid sponsorOrganisationUserId,
        SponsorAuthorisationsModificationsSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        string rtsId
    )
    {
        var modifications = projectModificationRepository.GetModificationsBySponsorOrganisationUser(searchQuery, pageNumber, pageSize, sortField, sortDirection, sponsorOrganisationUserId, rtsId);
        var totalCount = projectModificationRepository.GetModificationsBySponsorOrganisationUserCount(searchQuery, sponsorOrganisationUserId, rtsId);

        return Task.FromResult(new ModificationSearchResponse
        {
            Modifications = modifications.Adapt<IEnumerable<ModificationDto>>(),
            TotalCount = totalCount
        });
    }

    /// <summary>
    /// Saves modification review responses.
    /// </summary>
    /// <param name="modificationReviewRequest">The request object containing the review values</param>
    public Task SaveModificationReviewResponses(ModificationReviewRequest modificationReviewRequest)
    {
        return projectModificationRepository.SaveModificationReviewResponses(modificationReviewRequest, GetUserIdFromContext());
    }

    /// <summary>
    /// Gets modification review responses for a specific project modification.
    /// </summary>
    /// <param name="projectModificationId">The unique identifier of the modification</param>
    /// <returns>The modification review responses</returns>
    public async Task<ModificationReviewResponse?> GetModificationReviewResponses(string projectRecordId, Guid projectModificationId)
    {
        var specification = new GetModificationSpecification(projectRecordId, projectModificationId);

        var modification = await projectModificationRepository.GetModification(specification);

        if (modification == null)
        {
            return null;
        }

        return new ModificationReviewResponse
        {
            ModificationId = modification.Id,
            Comment = modification.ReviewerComments,
            ReasonNotApproved = modification.ReasonNotApproved,
            ReviewOutcome = modification.ProvisionalReviewOutcome,
            RevisionDescription = modification.RevisionDescription,
            ApplicantRevisionResponse = modification.ApplicantRevisionResponse,
            RequestForInformationReasons = [.. modification.ModificationRfiReasons.OrderBy(r => r.Sequence).Select(r => r.Reason)]
        };
    }

    public Task<ProjectOverviewDocumentResponse> GetDocumentsForModification
    (
        Guid modificationId,
        ProjectOverviewDocumentSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection
    )
    {
        var modifications = projectModificationRepository.GetDocumentsForModification(searchQuery, pageNumber, pageSize, sortField, sortDirection, modificationId);
        var totalCount = projectModificationRepository.GetDocumentsForModificationCount(searchQuery, modificationId);

        return Task.FromResult(new ProjectOverviewDocumentResponse
        {
            Documents = modifications.Adapt<IEnumerable<ProjectOverviewDocumentDto>>(),
            TotalCount = totalCount
        });
    }

    public async Task<ModificationResponse?> GetModification(string projectRecordId, Guid projectModificationId)
    {
        var specification = new GetModificationSpecification(projectRecordId, projectModificationId);

        var modification = await projectModificationRepository.GetModification(specification);

        return modification?.Adapt<ModificationResponse>();
    }

    private Guid GetUserIdFromContext()
    {
        return Guid.Parse(httpContextAccessor.HttpContext!.User!.Claims!.FirstOrDefault(x => x.Type == "userId")!.Value!);
    }
}