using Ardalis.Specification;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Contracts.Repositories;

/// <summary>
/// Repository abstraction for creating, querying and managing <see cref="ProjectModification"/> aggregates
/// and their related <see cref="ProjectModificationChange"/> entities.
/// </summary>
public interface IProjectModificationRepository
{
    /// <summary>
    /// Persists a new <see cref="ProjectModification"/> aggregate.
    /// </summary>
    /// <param name="projectModification">Fully populated modification entity to persist. The <see cref="ProjectModification.Id"/> may be pre-assigned (client) or generated (store) depending on implementation.</param>
    /// <returns>The created <see cref="ProjectModification"/> (including any store-assigned values).</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="projectModification"/> is null.</exception>
    Task<ProjectModification> CreateModification(ProjectModification projectModification);

    /// <summary>
    /// Persists a new <see cref="ProjectModificationChange"/> linked to an existing <see cref="ProjectModification"/>.
    /// </summary>
    /// <param name="projectModificationChange">The change entity to add. Must reference a valid parent <see cref="ProjectModification"/> via <see cref="ProjectModificationChange.ProjectModificationId"/>.</param>
    /// <returns>The created <see cref="ProjectModificationChange"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="projectModificationChange"/> is null.</exception>
    Task<ProjectModificationChange> CreateModificationChange(ProjectModificationChange projectModificationChange);

    /// <summary>
    /// Retrieves a single <see cref="ProjectModificationChange"/> using the provided specification.
    /// </summary>
    /// <param name="specification">Specification encapsulating the filtering criteria (typically by Id).</param>
    /// <returns>The matching change or null if not found.</returns>
    Task<ProjectModificationChange?> GetModificationChange(GetModificationChangeSpecification specification);

    /// <summary>
    /// Retrieves all <see cref="ProjectModificationChange"/> entities that satisfy the provided specification.
    /// </summary>
    /// <param name="specification">Specification defining the filtering logic (e.g. by parent modification Id).</param>
    /// <returns>An enumerable of matching change entities (empty if none).</returns>
    Task<IEnumerable<ProjectModificationChange>> GetModificationChanges(GetModificationChangesSpecification specification);

    /// <summary>
    /// Retrieves a paged, sorted list of project modifications matching the supplied search criteria.
    /// </summary>
    /// <param name="searchQuery">
    /// Composite filter object. The following fields may be applied (if non-null / non-empty):
    /// IrasId, ChiefInvestigatorName, ShortProjectTitle, SponsorOrganisation, FromDate (CreatedFrom),
    /// ToDate (CreatedTo), LeadNation (multi-value), ParticipatingNation (multi-value),
    /// ModificationTypes (multi-value), ReviewerId (when IncludeReviewerId = true).
    /// </param>
    /// <param name="pageNumber">1-based page number.</param>
    /// <param name="pageSize">Number of records per page. Implementations should guard against excessive values.</param>
    /// <param name="sortField">Property / column name to sort by (case-insensitive). Implementations should validate against a whitelist.</param>
    /// <param name="sortDirection">Sort direction: typically "ASC" or "DESC" (case-insensitive).</param>
    /// <param name="projectRecordId">Optional additional filter restricting results to a specific project record.</param>
    /// <returns>Paged sequence of <see cref="ProjectModificationResult"/> projections.</returns>
    /// <remarks>
    /// Expected to perform server-side filtering, ordering and paging for efficiency.
    /// If <paramref name="pageNumber"/> or <paramref name="pageSize"/> are invalid, implementations may normalize them.
    /// </remarks>
    IEnumerable<ProjectModificationResult> GetModifications
    (
        ModificationSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        string? projectRecordId = null
    );

    /// <summary>
    /// Retrieves a paged, sorted list of sponsor organisation user modifications matching the supplied search criteria.
    /// </summary>
    /// <param name="searchQuery">Object containing filtering criteria for modifications.</param>
    /// <param name="pageNumber">1-based page number.</param>
    /// <param name="pageSize">Number of records per page. Implementations should guard against excessive values.</param>
    /// <param name="sortField">Property / column name to sort by (case-insensitive). Implementations should validate against a whitelist.</param>
    /// <param name="sortDirection">Sort direction: typically "ASC" or "DESC" (case-insensitive).</param>
    /// <param name="sponsorOrganisationUserId">The unique identifier of the sponsor organisation user for which modifications are requested</param>
    /// <returns>Paged sequence of <see cref="ProjectModificationResult"/> projections.</returns>
    /// <remarks>
    /// Expected to perform server-side filtering, ordering and paging for efficiency.
    /// If <paramref name="pageNumber"/> or <paramref name="pageSize"/> are invalid, implementations may normalize them.
    /// </remarks>
    IEnumerable<ProjectModificationResult> GetModificationsBySponsorOrganisationUser
    (
       SponsorAuthorisationsModificationsSearchRequest searchQuery,
       int pageNumber,
       int pageSize,
       string sortField,
       string sortDirection,
       Guid sponsorOrganisationUserId
    );

    /// <summary>
    /// Returns the total count of modifications matching the supplied search criteria (ignoring paging).
    /// </summary>
    /// <param name="searchQuery">Same filtering contract as <see cref="GetModificationsBySponsorOrganisationUser"/>.</param>
    /// <param name="sponsorOrganisationUserId">Sponsor organisation user unique identifier.</param>
    /// <returns>Total number of matching records.</returns>
    int GetModificationsBySponsorOrganisationUserCount(SponsorAuthorisationsModificationsSearchRequest searchQuery, Guid sponsorOrganisationUserId);

    /// <summary>
    /// Returns the total count of modifications matching the supplied search criteria (ignoring paging).
    /// </summary>
    /// <param name="searchQuery">Same filtering contract as <see cref="GetModifications"/>.</param>
    /// <param name="projectRecordId">Optional project record constraint.</param>
    /// <returns>Total number of matching records.</returns>
    int GetModificationsCount(ModificationSearchRequest searchQuery, string? projectRecordId = null);

    /// <summary>
    /// Retrieves multiple modification projections by their identifiers.
    /// </summary>
    /// <param name="Ids">Collection of modification identifiers (string form). Empty list returns empty result.</param>
    /// <returns>Matching <see cref="ProjectModificationResult"/> items (order not guaranteed unless enforced by implementation).</returns>
    IEnumerable<ProjectModificationResult> GetModificationsByIds(List<string> Ids);

    /// <summary>
    /// Assigns (or reassigns) a set of modifications to a reviewer.
    /// </summary>
    /// <param name="modificationIds">List of modification identifiers to assign.</param>
    /// <param name="reviewerId">Identifier of the reviewer user/principal.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task AssignModificationsToReviewer(List<string> modificationIds, string reviewerId, string reviewerEmail, string reviewerName);

    IEnumerable<ProjectOverviewDocumentResult> GetDocumentsForProjectOverview
    (
        ProjectOverviewDocumentSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        string? projectRecordId = null
    );

    int GetDocumentsForProjectOverviewCount(ProjectOverviewDocumentSearchRequest searchQuery, string? projectRecordId = null);

    /// <summary>
    /// Removes one or more <see cref="ProjectModificationChange"/> entities that satisfy the provided specification.
    /// </summary>
    /// <param name="specification">Specification that identifies the change(s) to remove. Must not be null.</param>
    /// <returns>Task representing the asynchronous delete operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="specification"/> is null.</exception>
    /// <remarks>
    /// Implementations should treat this as an idempotent operation: if no entities match, the method completes without error.
    /// Implementations may choose to remove all matches or enforce that the specification uniquely identifies a single entity.
    /// </remarks>
    Task RemoveModificationChange(ISpecification<ProjectModificationChange> specification);

    /// <summary>
    /// Updates the status <see cref="ProjectModification"/> of the modification that matches the provided specification.
    /// It also updates the status of the modification changes for this modification, to keep in sync
    /// If no matching entity is found, the method completes without making any changes.
    /// </summary>
    /// <param name="specification">The specification used to locate the modification to update.</param>
    Task UpdateModificationStatus(ISpecification<ProjectModification> specification, string status, string? revisionDescription);

    /// <summary>
    /// Updates the <see cref="ProjectModification"/> modification that matches the provided specification.
    /// Only updates fields that are explicitly set (non-null) in the specification.
    /// If no matching entity is found, the method completes without making any changes.
    /// </summary>
    /// <param name="specification">The specification used to locate the modification to update.</param>
    /// <param name="projectModification">The <see cref="ProjectModification"/> entity with updated values.</param>
    Task UpdateModification(ISpecification<ProjectModification> specification, ProjectModification projectModification);

    /// <summary>
    /// Updates an existing <see cref="ProjectModificationChange"/> entity.
    /// Only updates fields that are explicitly set (non-null) in the change object.
    /// If the change is null, or if no change matches the identifier, the method completes without making any changes.
    /// </summary>
    /// <param name="modificationChange">The <see cref="ProjectModificationChange"/> entity with updated values. Identifier must be set to match the change to update.</param>
    Task UpdateModificationChange(ISpecification<ProjectModificationChange> specification, ProjectModificationChange modificationChange);

    /// <summary>
    /// Deletes the  <see cref="ProjectModification"/> modification that matches the provided specification.
    /// </summary>
    /// <param name="specification">The specification used to locate the modification to delete.</param>
    Task DeleteModification(ISpecification<ProjectModification> specification);

    Task<IEnumerable<ProjectModificationAuditTrail>> GetModificationAuditTrail(Guid modificationId);

    /// <summary>
    /// Saves the modification review responses
    /// </summary>
    /// <param name="modificationReviewRequest">The request object containing the review values</param>
    Task SaveModificationReviewResponses(ModificationReviewRequest modificationReviewRequest);

    IEnumerable<ProjectOverviewDocumentResult> GetDocumentsForModification
    (
        ProjectOverviewDocumentSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        Guid modificationId
    );

    int GetDocumentsForModificationCount(ProjectOverviewDocumentSearchRequest searchQuery, Guid modificationId);

    /// <summary>
    /// Retrieves a specific project modification by its unique identifier.
    /// </summary>
    /// <param name="specification">The specification used to locate the project modification.</param>
    /// <returns>The matching <see cref="ProjectModification"/> entity, or null if not found.</returns>
    Task<ProjectModification?> GetModification(GetModificationSpecification specification);
}