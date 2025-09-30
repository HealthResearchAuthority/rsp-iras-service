using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize]
public class DocumentsController(IMediator mediator) : ControllerBase
{
    [HttpPost("updatedocumentscanstatus")]
    public async Task<IActionResult> UpdateDocumentScanStatus([FromBody] ModificationDocumentDto dto)
    {
        // Validation failure → 400
        if (dto.Id == Guid.Empty || string.IsNullOrWhiteSpace(dto.DocumentStoragePath))
        {
            var bad = new UpdateDocumentScanStatusResponse
            {
                Id = dto.Id,
                CorellationId = dto.CorellationId,
                Status = "failure",
                Timestamp = DateTime.UtcNow,
                Message = "Validation failed.",
                ErrorResponse = new ErrorResponse
                {
                    Code = "VALIDATION_ERROR",
                    Message = "Either Id or DocumentStoragePath must be provided.",
                    Details = "Id must be a non-empty GUID and DocumentStoragePath must be provided."
                }
            };

            return BadRequest(bad);
        }

        var request = new UpdateModificationDocumentCommand(dto);
        var result = await mediator.Send(request);

        if (result != null)
        {
            // Success → 200 (no ErrorResponse serialized)
            return Ok(new UpdateDocumentScanStatusResponse
            {
                Id = dto.Id,
                CorellationId = dto.CorellationId,
                Status = "success",
                Timestamp = DateTime.UtcNow,
                Message = "Malware scan completed successfully. Document is clean."
            });
        }

        return BadRequest(new UpdateDocumentScanStatusResponse
        {
            Id = dto.Id,
            CorellationId = dto.CorellationId,
            Status = "failure",
            Timestamp = DateTime.UtcNow,
            Message = "An unexpected error occurred. Malware scan was not completed",
            ErrorResponse = new ErrorResponse
            {
                Code = "SERVER_ERROR",
                Message = "Unexpected server error."
            }
        });
    }
}