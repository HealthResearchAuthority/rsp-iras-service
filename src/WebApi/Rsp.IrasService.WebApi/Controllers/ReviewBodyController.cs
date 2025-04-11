using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        var request = new UpdateReviewBodyCommand(reviewBodyDto);
        var updateReviewBodyResult = await mediator.Send(request);

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

        return enableReviewBodyResult;
    }
}