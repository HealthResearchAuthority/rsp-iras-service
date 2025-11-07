using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProjectModificationsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Gets all modifications with filtering, sorting and pagination
    /// <param name="searchQuery">Object containing filtering criteria for modifications.</param>
    /// <param name="pageNumber">The number of the page to retrieve (used for pagination - 1-based).</param>
    /// <param name="pageSize">The number of items per page (used for pagination).</param>
    /// <param name="sortField">The field name by which the results should be sorted.</param>
    /// <param name="sortDirection">The direction of sorting: "asc" for ascending or "desc" for descending.</param>
    /// <returns>Returns a paginated list of modifications matching the search criteria.</returns>
    [HttpPost("getallmodifications")]
    public async Task<ActionResult<ModificationResponse>> GetAllModifications(
        [FromBody] ModificationSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection)
    {
        if (pageNumber <= 0)
        {
            return BadRequest("pageIndex must be greater than 0.");
        }
        if (pageSize <= 0)
        {
            return BadRequest("pageSize must be greater than 0.");
        }

        var query = new GetModificationsQuery(searchQuery, pageNumber, pageSize, sortField, sortDirection);

        return await mediator.Send(query);
    }

    /// <summary>
    /// Gets modifications for specific ProjectRecordId with filtering, sorting and pagination
    /// </summary>
    /// <param name="projectRecordId">The unique identifier of the project record for which modifications are requested.</param>
    /// <param name="searchQuery">Object containing filtering criteria for modifications.</param>
    /// <param name="pageNumber">The number of the page to retrieve (used for pagination - 1-based).</param>
    /// <param name="pageSize">The number of items per page (used for pagination).</param>
    /// <param name="sortField">The field name by which the results should be sorted.</param>
    /// <param name="sortDirection">The direction of sorting: "asc" for ascending or "desc" for descending.</param>
    /// <returns>Returns a paginated list of modifications related to the specified project record.</returns>
    [HttpPost("getmodificationsforproject")]
    public async Task<ActionResult<ModificationResponse>> GetModificationsForProject(
        string projectRecordId,
        [FromBody] ModificationSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection)
    {
        if (pageNumber <= 0)
        {
            return BadRequest("pageIndex must be greater than 0.");
        }
        if (pageSize <= 0)
        {
            return BadRequest("pageSize must be greater than 0.");
        }

        var query = new GetModificationsForProjectQuery(projectRecordId, searchQuery, pageNumber, pageSize, sortField, sortDirection);

        return await mediator.Send(query);
    }

    /// <summary>
    /// Retrieves modifications by a list of modification IDs.
    /// </summary>
    /// <param name="Ids">A list of IDs relating to modifications</param>
    /// <returns>A list of modifications corresponding to the provided IDs</returns>
    [HttpPost("getmodificationsbyids")]
    public async Task<ActionResult<ModificationResponse>> GetModificationsByIds([FromBody] List<string> Ids)
    {
        if (Ids is null || Ids.Count == 0)
        {
            return BadRequest("no IDs provided.");
        }

        var query = new GetModificationsByIdsQuery(Ids);

        return await mediator.Send(query);
    }

    /// <summary>
    /// Creates a new project modification.
    /// </summary>
    /// <param name="modificationRequest">The request object containing modification details.</param>
    /// <returns>The created modification response.</returns>
    [HttpPost]
    public async Task<ModificationResponse> CreateModification(ModificationRequest modificationRequest)
    {
        var request = new CreateModificationCommand(modificationRequest);

        return await mediator.Send(request);
    }

    /// <summary>
    /// Creates a new change for a project modification.
    /// </summary>
    /// <param name="modificationChangeRequest">The request object containing modification change details.</param>
    /// <returns>The created modification change response.</returns>
    [HttpPost("change")]
    public async Task<ModificationChangeResponse> CreateModificationChange(ModificationChangeRequest modificationChangeRequest)
    {
        var request = new SaveModificationChangeCommand(modificationChangeRequest);

        return await mediator.Send(request);
    }

    /// <summary>
    /// Retrieves a modification change by its unique identifier.
    /// </summary>
    /// <param name="modificationChangeId">The unique identifier of the modification change to retrieve.</param>
    /// <returns>The modification change that matches the provided identifier.</returns>
    [HttpGet("change")]
    public async Task<ModificationChangeResponse> GetModificationChange(Guid modificationChangeId)
    {
        var request = new GetModificationChangeQuery(modificationChangeId);

        return await mediator.Send(request);
    }

    /// <summary>
    /// Retrieves all modification changes associated with the specified project modification.
    /// </summary>
    /// <param name="projectModificationId">The unique identifier of the project modification whose changes are being retrieved.</param>
    /// <returns>A collection of modification change responses linked to the specified project modification.</returns>
    [HttpGet("changes")]
    public async Task<IEnumerable<ModificationChangeResponse>> GetModificationChanges(Guid projectModificationId)
    {
        var request = new GetModificationChangesQuery(projectModificationId);

        return await mediator.Send(request);
    }

    /// <summary>
    /// Creates a new change for a project modification.
    /// </summary>
    /// <param name="modificationChangeRequest">The request object containing modification change details.</param>
    /// <returns>The created modification change response.</returns>
    [HttpPost("createdocument")]
    public async Task CreateModificationDocument(List<ModificationDocumentDto> modificationChangeRequest)
    {
        var request = new SaveModificationDocumentsCommand(modificationChangeRequest);

        await mediator.Send(request);
    }

    [HttpPost("assignmodificationstoreviewer")]
    public async Task AssignModificationsToReviewer(List<string> modificationsIds, string reviewerId, string reviewerEmail, string reviewerName)
    {
        var command = new AssignModificationsToReviewerCommand(modificationsIds, reviewerId, reviewerEmail, reviewerName);
        await mediator.Send(command);
    }

    /// <summary>
    /// Gets modifications for specific ProjectRecordId with filtering, sorting and pagination
    /// </summary>
    /// <param name="projectRecordId">The unique identifier of the project record for which modifications are requested.</param>
    /// <param name="searchQuery">Object containing filtering criteria for modifications.</param>
    /// <param name="pageNumber">The number of the page to retrieve (used for pagination - 1-based).</param>
    /// <param name="pageSize">The number of items per page (used for pagination).</param>
    /// <param name="sortField">The field name by which the results should be sorted.</param>
    /// <param name="sortDirection">The direction of sorting: "asc" for ascending or "desc" for descending.</param>
    /// <returns>Returns a paginated list of modifications related to the specified project record.</returns>
    [HttpPost("getdocumentsforprojectoverview")]
    public async Task<ActionResult<ProjectOverviewDocumentResponse>> GetModificationDocumentsForProject(
        string projectRecordId,
        [FromBody] ProjectOverviewDocumentSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection)
    {
        if (pageNumber <= 0)
        {
            return BadRequest("pageIndex must be greater than 0.");
        }
        if (pageSize <= 0)
        {
            return BadRequest("pageSize must be greater than 0.");
        }

        var query = new GetDocumentsForProjectOverviewQuery(projectRecordId, searchQuery, pageNumber, pageSize, sortField, sortDirection);

        return await mediator.Send(query);
    }

    /// <summary>
    /// Deletes a modification change by its unique identifier.
    /// </summary>
    /// <param name="modificationChangeId">The unique identifier of the modification change to delete.</param>
    [HttpDelete("remove")]
    public async Task RemoveModificationChange(Guid modificationChangeId)
    {
        var request = new RemoveModificationChangeCommand(modificationChangeId);

        await mediator.Send(request);
    }

    /// <summary>
    /// Updates a modification by its unique identifier.
    /// </summary>
    /// <param name="modificationId">The unique identifier of the modification to update.</param>
    /// <param name="status">The new status for modification to update.</param>
    [HttpPost("update")]
    public async Task UpdateModificationStatus(Guid modificationId, string status)
    {
        var request = new UpdateModificationStatusCommand(modificationId, status);

        await mediator.Send(request);
    }

    /// <summary>
    /// Deletes documents for a project modification.
    /// </summary>
    /// <param name="modificationChangeRequest">The request object containing modification change details.</param>
    /// <returns>The created modification change response.</returns>
    [HttpPost("deletedocuments")]
    public async Task DeleteDocuments(List<ModificationDocumentDto> modificationChangeRequest)
    {
        var request = new DeleteModificationDocumentsCommand(modificationChangeRequest);

        await mediator.Send(request);
    }

    /// <summary>
    /// Delete a modification by its unique identifier.
    /// </summary>
    /// <param name="modificationId">The unique identifier of the modification to update.</param>
    [HttpPost("delete")]
    public async Task DeleteModification(Guid modificationId)
    {
        var request = new DeleteModificationCommand(modificationId);

        await mediator.Send(request);
    }

    [HttpGet("audittrail")]
    public async Task<ModificationAuditTrailResponse> GetModificationAuditTrail(Guid modificationId)
    {
        var query = new GetModificationAuditTrailQuery(modificationId);
        return await mediator.Send(query);
    }

    /// <summary>
    /// Gets modifications for specific SponsorOrganisationUserId with filtering, sorting and pagination
    /// </summary>
    /// <param name="sponsorOrganisationUserId">The unique identifier of the sponsor organisation user for which modifications are requested.</param>
    /// <param name="searchQuery">Object containing filtering criteria for modifications.</param>
    /// <param name="pageNumber">The number of the page to retrieve (used for pagination - 1-based).</param>
    /// <param name="pageSize">The number of items per page (used for pagination).</param>
    /// <param name="sortField">The field name by which the results should be sorted.</param>
    /// <param name="sortDirection">The direction of sorting: "asc" for ascending or "desc" for descending.</param>
    /// <returns>Returns a paginated list of modifications related to the SponsorOrganisationUserId.</returns>
    [HttpPost("getmodificationsbysponsororganisationuserid")]
    public async Task<ActionResult<ModificationResponse>> GetModificationsBySponsorOrganisationUserId(
        Guid sponsorOrganisationUserId,
        [FromBody] SponsorAuthorisationsSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection)
    {
        if (pageNumber <= 0)
        {
            return BadRequest("pageIndex must be greater than 0.");
        }
        if (pageSize <= 0)
        {
            return BadRequest("pageSize must be greater than 0.");
        }

        var query = new GetModificationsBySponsorOrganisationUserIdQuery(sponsorOrganisationUserId, searchQuery, pageNumber, pageSize, sortField, sortDirection);

        return await mediator.Send(query);
    }
}