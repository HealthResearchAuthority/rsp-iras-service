using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProjectClosureController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates a project closure record
    /// </summary>
    /// <param name="projectClosureRequest">Research project closure request</param>
    [HttpPost("createprojectclosure")]
    public async Task<ProjectClosureResponse> CreateProjectClosure(ProjectClosureRequest projectClosureRequest)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

        projectClosureRequest.UserId = userId!;

        var request = new CreateProjectClosureCommand(projectClosureRequest);

        return await mediator.Send(request);
    }

    /// <summary>
    /// Returns a project closure search response
    /// </summary>
    /// <param name="projectRecordId">Researcher Project Record Id</param>
    [HttpGet("getprojectclosuresbyid")]
    public async Task<ActionResult<ProjectClosuresSearchResponse>> GetProjectClosuresByProjectRecordId(string projectRecordId)
    {
        var request = new GetProjectClosureQuery(projectRecordId);

        return await mediator.Send(request);
    }

    /// <summary>
    /// Gets project closures records for specific sponsorOrganisationUserId with filtering, sorting and pagination
    /// </summary>
    /// <param name="sponsorOrganisationUserId">The unique identifier of the sponsor organisation user for which project closures are requested.</param>
    /// <param name="searchQuery">Object containing filtering criteria for project closures.</param>
    /// <param name="pageNumber">The number of the page to retrieve (used for pagination - 1-based).</param>
    /// <param name="pageSize">The number of items per page (used for pagination).</param>
    /// <param name="sortField">The field name by which the results should be sorted.</param>
    /// <param name="sortDirection">The direction of sorting: "asc" for ascending or "desc" for descending.</param>
    /// <returns>Returns a paginated list of project closures.</returns>
    [HttpPost("getprojectclosuresbysponsororganisationuserid")]
    public async Task<ActionResult<ProjectClosuresSearchResponse>> GetProjectClosuresBySponsorOrganisationUserId
    (
        Guid sponsorOrganisationUserId,
        [FromBody] ProjectClosuresSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection
    )
    {
        if (pageNumber <= 0)
        {
            return BadRequest("pageIndex must be greater than 0.");
        }
        if (pageSize <= 0)
        {
            return BadRequest("pageSize must be greater than 0.");
        }

        var query = new GetProjectClosuresBySponsorOrganisationUserIdQuery(sponsorOrganisationUserId, searchQuery, pageNumber, pageSize, sortField, sortDirection);

        return await mediator.Send(query);
    }

    /// <summary>
    /// Updates the project closure status to either Authorised or Not authorised.
    /// If the status is set to Authorised, the method also closes the associated project record
    /// by updating its status to Closed.
    /// </summary>
    /// <param name="projectRecordId">The unique identifier of the project record whose closure status will be updated.</param>
    /// <param name="status">The new closure status to apply (Authorised or Not authorised).</param>
    /// <returns>An API response indicating the result of the operation.</returns>
    [HttpPatch("updateprojectclosurestatus")]
    public async Task UpdateProjectClosureStatus(string projectRecordId, string status)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

        var request = new UpdateProjectClosureStatusCommand
        {
            ProjectRecordId = projectRecordId,
            Status = status,
            UserId = userId
        };

        await mediator.Send(request);
    }
}