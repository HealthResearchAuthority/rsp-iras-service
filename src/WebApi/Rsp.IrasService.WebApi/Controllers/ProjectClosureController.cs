using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProjectClosureController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates a project closure record
    /// </summary>
    /// <param name="projectClosureRequest">Research project closure request</param>
    [HttpPost]
    public async Task<ProjectClosureResponse> CreateProjectClosure(ProjectClosureRequest projectClosureRequest)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

        projectClosureRequest.UserId = userId!;

        var request = new CreateProjectClosureCommand(projectClosureRequest);

        return await mediator.Send(request);
    }

    /// <summary>
    /// Returns a single project closure record
    /// </summary>
    /// <param name="projectRecordId">Researcher Project Record Id</param>
    [HttpGet("{projectRecordId}")]
    [Produces<ProjectRecord>]
    public async Task<ActionResult<ProjectClosureResponse>> GetProjectClosure(string projectRecordId)
    {
        var request = new GetProjectClosureQuery(projectRecordId);

        var response = await mediator.Send(request);

        return response == null ? NotFound() : Ok(response);
    }
}