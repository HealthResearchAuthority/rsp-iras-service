using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class DocumentsController(IMediator mediator) : ControllerBase
{
    [HttpPost("updatedocumentscanstatus")]
    public async Task<IActionResult> UpdateDocumentScanStatus([FromBody] ModificationDocumentDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.DocumentStoragePath))
        {
            var bad = new UpdateDocumentScanStatusResponse
            {
                Id = dto.Id,
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

        try
        {
            var request = new UpdateModificationDocumentCommand(dto);
            var result = await mediator.Send(request);

            return result switch
            {
                StatusCodes.Status200OK => Ok(new UpdateDocumentScanStatusResponse
                {
                    Id = dto.Id,
                    Status = "success",
                    Timestamp = DateTime.UtcNow,
                    Message = "Malware scan completed successfully."
                }),
                StatusCodes.Status404NotFound => NotFound(new UpdateDocumentScanStatusResponse
                {
                    Id = dto.Id,
                    Status = "failure",
                    Timestamp = DateTime.UtcNow,
                    Message = "An unexpected error occurred. Malware scan was not completed",
                    ErrorResponse = new ErrorResponse
                    {
                        Code = "NOT_FOUND",
                        Message = "Document could not be found using document storage path."
                    }
                }),
                _ => throw new InvalidOperationException($"Unhandled status code: {result}")
            };
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new UpdateDocumentScanStatusResponse
                {
                    Id = dto.Id,
                    Status = "failure",
                    Timestamp = DateTime.UtcNow,
                    Message = "Internal server error occurred while updating document scan status.",
                    ErrorResponse = new ErrorResponse
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Message = ex.Message,
                        Details = ex.ToString() // stack trace & more details
                    }
                });
        }
    }
}