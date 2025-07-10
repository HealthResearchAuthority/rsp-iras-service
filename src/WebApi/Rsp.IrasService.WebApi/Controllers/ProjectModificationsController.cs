using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProjectModificationsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates a research application
    /// </summary>
    /// <param name="modificationRequest">Research Application Request</param>
    [HttpPost]
    public async Task<ModificationResponse> CreateModification(ModificationRequest modificationRequest)
    {
        var request = new CreateModificationCommand(modificationRequest);

        return await mediator.Send(request);
    }

    /// <summary>
    /// Creates a research application
    /// </summary>
    /// <param name="modificationChangeRequest">Research Application Request</param>
    [HttpPost("change")]
    public async Task<ModificationChangeResponse> CreateModificationChange(ModificationChangeRequest modificationChangeRequest)
    {
        var request = new SaveModificationChangeCommand(modificationChangeRequest);

        return await mediator.Send(request);
    }
}