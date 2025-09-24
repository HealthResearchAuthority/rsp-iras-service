using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Domain.Entities;

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
    /// Returns all area of changes and specific area of changes for a project modification change.
    /// </summary>
    [HttpGet("areaofchanges")]
    [Produces<IEnumerable<ModificationAreaOfChange>>]
    public async Task<IEnumerable<ModificationAreaOfChangeDto>> GetAreaOfChanges()
    {
        var query = new GetModificationAreaOfChangeQuery();

        return await mediator.Send(query);
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
    public async Task AssignModificationsToReviewer(List<string> modificationsIds, string reviewerId)
    {
        var command = new AssignModificationsToReviewerCommand(modificationsIds, reviewerId);
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
}