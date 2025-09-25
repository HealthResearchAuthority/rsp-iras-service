using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class DocumentsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     Gets all modifications with filtering, sorting and pagination
    ///     <param name="searchQuery">Object containing filtering criteria for modifications.</param>
    ///     <param name="pageNumber">The number of the page to retrieve (used for pagination - 1-based).</param>
    ///     <param name="pageSize">The number of items per page (used for pagination).</param>
    ///     <param name="sortField">The field name by which the results should be sorted.</param>
    ///     <param name="sortDirection">The direction of sorting: "asc" for ascending or "desc" for descending.</param>
    ///     <returns>Returns a paginated list of modifications matching the search criteria.</returns>
    [HttpPost("updatedocumentscanstatus")]
    public async Task<IActionResult> UpdateDocumentScanStatus(
        [FromBody] ModificationDocumentDto searchQuery)
    {
        if (searchQuery.Id == Guid.Empty || string.IsNullOrWhiteSpace(searchQuery.DocumentStoragePath))
        {
            return BadRequest("Either Id or DocumentStoragePath must be provided.");
        }

        var query = new UpdateModificationDocumentCommand(searchQuery);
        await mediator.Send(query);

        return Ok();
    }
}