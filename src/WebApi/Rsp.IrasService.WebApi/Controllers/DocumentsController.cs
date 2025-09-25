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
public class DocumentsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Gets all modifications with filtering, sorting and pagination
    /// <param name="searchQuery">Object containing filtering criteria for modifications.</param>
    /// <param name="pageNumber">The number of the page to retrieve (used for pagination - 1-based).</param>
    /// <param name="pageSize">The number of items per page (used for pagination).</param>
    /// <param name="sortField">The field name by which the results should be sorted.</param>
    /// <param name="sortDirection">The direction of sorting: "asc" for ascending or "desc" for descending.</param>
    /// <returns>Returns a paginated list of modifications matching the search criteria.</returns>
    [HttpPost("updatedocumentscanstatus")]
    public async Task<ActionResult<ModificationResponse>> updatedocumentscanstatus(
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
}