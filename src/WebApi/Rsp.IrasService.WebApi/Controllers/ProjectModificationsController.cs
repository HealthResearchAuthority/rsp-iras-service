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
}