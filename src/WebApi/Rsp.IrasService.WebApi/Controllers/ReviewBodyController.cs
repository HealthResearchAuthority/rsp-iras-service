using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.Constants;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ReviewBodyController(IMediator mediator, IReviewBodyAuditTrailService auditService) : ControllerBase
{
    /// <summary>
    ///     Returns all review bodies
    /// </summary>
    [HttpGet]
    [HttpGet("{id}")]
    [Produces<IEnumerable<ReviewBody>>]
    public async Task<IEnumerable<ReviewBodyDto>> GetReviewBodies(Guid? id = null)
    {
        var query = id == null ?
            new GetReviewBodiesQuery() :
            new GetReviewBodiesQuery(id.Value);

        return await mediator.Send(query);
    }

    /// <summary>
    ///     Creates a review body
    /// </summary>
    /// <param name="reviewBodyDto">Research Body Dto</param>
    [HttpPost("create")]
    public async Task<ReviewBodyDto> Create
        (ReviewBodyDto reviewBodyDto)
    {
        var request = new CreateReviewBodyCommand(reviewBodyDto);
        var createReviewBodyResult = await mediator.Send(request);

        // log audit trail
        var userId = UserEmail(User);
        var auditRecord = auditService.GenerateAuditTrailDtoFromReviewBody(
            createReviewBodyResult,
            userId!,
            ReviewBodyAuditTrailActions.Create
        );
        var loggedAuditTrail = await auditService.LogRecords(auditRecord);

        return createReviewBodyResult;
    }

    /// <summary>
    ///     Update a review body
    /// </summary>
    /// <param name="reviewBodyDto">Research Body Dto</param>
    [HttpPost("update")]
    public async Task<ReviewBodyDto> Update
        (ReviewBodyDto reviewBodyDto)
    {
        // get current object
        var currentReviewBodies = await mediator.Send(new GetReviewBodiesQuery(reviewBodyDto.Id));
        var currentReviewBody = currentReviewBodies.FirstOrDefault();

        var request = new UpdateReviewBodyCommand(reviewBodyDto);
        var updateReviewBodyResult = await mediator.Send(request);

        // log audit trail
        var userId = UserEmail(User);

        var auditRecord = auditService.GenerateAuditTrailDtoFromReviewBody(
            updateReviewBodyResult,
            userId!,
            ReviewBodyAuditTrailActions.Update,
            currentReviewBody
        );

        var loggedAuditTrail = await auditService.LogRecords(auditRecord);

        return updateReviewBodyResult;
    }

    /// <summary>
    ///     Disable a review body
    /// </summary>
    /// <param name="reviewBodyDto">Research Body Dto</param>
    [HttpPut("disable/{id}")]
    public async Task<ReviewBodyDto?> Disable(Guid id)
    {
        var request = new DisableReviewBodyCommand(id);
        var disableReviewBodyResult = await mediator.Send(request);

        // log audit trail
        var userId = UserEmail(User);
        if (disableReviewBodyResult != null)
        {
            var auditRecord = auditService.GenerateAuditTrailDtoFromReviewBody(
                disableReviewBodyResult,
                userId!,
                ReviewBodyAuditTrailActions.Disable
            );

            var loggedAuditTrail = await auditService.LogRecords(auditRecord);
        }

        return disableReviewBodyResult;
    }

    [HttpGet("audittrail")]
    public async Task<ReviewBodyAuditTrailResponse> GetAuditTrailForReviewBody(Guid id, int skip, int take)
    {
        var result = await auditService.GetAuditTrailForReviewBody(id, skip, take);

        return result;
    }

    /// <summary>
    ///     Enable a review body
    /// </summary>
    /// <param name="reviewBodyDto">Research Body Dto</param>
    [HttpPut("enable/{id}")]
    public async Task<ReviewBodyDto?> Enable(Guid id)
    {
        var request = new EnableReviewBodyCommand(id);
        var enableReviewBodyResult = await mediator.Send(request);

        // log audit trail
        var userId = UserEmail(User);
        if (enableReviewBodyResult != null)
        {
            var auditRecord = auditService.GenerateAuditTrailDtoFromReviewBody(
                enableReviewBodyResult,
                userId!,
                ReviewBodyAuditTrailActions.Enable
            );

            var loggedAuditTrail = await auditService.LogRecords(auditRecord);
        }

        return enableReviewBodyResult;
    }

    // gets the currently logged in user's email from the user claims
    private static string? UserEmail(ClaimsPrincipal user)
    {
        return user?.Claims
            ?.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")
            ?.Value;
    }
}