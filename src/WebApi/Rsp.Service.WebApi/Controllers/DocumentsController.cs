using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rsp.Service.Application.Constants;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Constants;

namespace Rsp.Service.WebApi.Controllers;

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
                    Message = "DocumentStoragePath must be provided.",
                    Details = "Document Storage Path was Empty. DocumentStoragePath must be provided."
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
                    Message = "Malware scan status updated successfully."
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

    /// <summary>
    /// This endpoint checks if the current user has access to the document associated with the given modification ID.
    /// and allow them to download.
    /// </summary>
    /// <param name="modificationId">The modification identifier.</param>
    [ExcludeFromCodeCoverage]
    [HttpGet("access/{modificationId}")]
    public async Task<IActionResult> DocumentAccess
    (
        [FromServices] IProjectModificationRepository projectModificationRepository,
        Guid modificationId
    )
    {
        if
        (
            // SystemAdministrator is a privileged role so access check is bypassed.
            // The applicant and sponsor roles are granted access via AccessMiddleware check
            // so if they land here, they are authorized to see document.
            User.IsInRole(Roles.SystemAdministrator) ||
            User.IsInRole(Roles.Sponsor) ||
            User.IsInRole(Roles.Applicant)
        )
        {
            return Ok();
        }

        // TeamManager, WorkflowCoordinator, and StudyWideReviewer roles
        // don't go via AccessMiddleware check, so we need to handle them here.
        // they should not be able to access documents in draft or with sponsor modifications.
        var specification = new GetModificationSpecification(null, modificationId);

        var modification = await projectModificationRepository.GetModification(specification);

        if (modification == null)
        {
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        // access is forbidden for InDraft or WithSponsor modifications
        if
        (
            modification is
            {
                Status: ModificationStatus.InDraft or
                        ModificationStatus.WithSponsor
            }
        )
        {
            return new StatusCodeResult(StatusCodes.Status403Forbidden);
        }

        return Ok(modification);
    }
}