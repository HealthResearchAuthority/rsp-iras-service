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
//[Authorize]
public class ReviewBodyController(IMediator mediator, IReviewBodyAuditTrailService auditService) : ControllerBase
{
    [HttpPost("all")]
    [Produces<AllReviewBodiesResponse>]
    public async Task<AllReviewBodiesResponse> GetAllReviewBodies(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize, 
        [FromQuery] string sortField,
        [FromQuery] string sortDirection,
        [FromBody] ReviewBodySearchRequest searchQuery)
    {
        var query = new GetReviewBodiesQuery(pageNumber, pageSize, sortField, sortDirection, searchQuery);
        return await mediator.Send(query);
    }

    [HttpPost("allactive")]
    [Produces<AllReviewBodiesResponse>]
    public async Task<AllReviewBodiesResponse> GetAllActiveReviewBodies(
        [FromQuery] string sortField,
        [FromQuery] string sortDirection
        )
    {
         ReviewBodySearchRequest searchQuery =
        new ReviewBodySearchRequest
        {
            Status = true
        };
        var query = new GetReviewBodiesQuery(1, int.MaxValue, sortField, sortDirection, searchQuery);
        return await mediator.Send(query);
    }

    /// <summary>
    ///     Returns review body by ID
    /// </summary>
    [HttpGet]
    [HttpGet("{id}")]
    [Produces<RegulatoryBody>]
    public async Task<ReviewBodyDto> GetReviewBody(Guid id)
    {
        var query = new GetReviewBodyQuery(id);

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
    [HttpPut("enable/{id}")]
    public async Task<ReviewBodyDto?> Enable(Guid id)
    {
        var request = new EnableReviewBodyCommand(id);
        var enableReviewBodyResult = await mediator.Send(request);

        return enableReviewBodyResult;
    }

    /// <summary>
    ///     Get review body users by user id
    /// </summary>
    /// <param name="reviewBodyUser">Review Body User Dto</param>
    [HttpGet("allbyuser/{id}")]
    public async Task<List<ReviewBodyUserDto>> GetUserReviewBodies(Guid id)
    {
        var request = new GetReviewBodyUserCommand(id);
        var reviewBodyUserDtos = await mediator.Send(request);

        return reviewBodyUserDtos;
    }

    /// <summary>
    ///     Remove a user from a review body
    /// </summary>
    [HttpPost("removeuser")]
    public async Task<ReviewBodyUserDto?> RemoveUser(Guid reviewBodyId, Guid userId)
    {
        var request = new RemoveReviewBodyUserCommand(reviewBodyId, userId);
        var removeuser = await mediator.Send(request);

        return removeuser;
    }

    /// <summary>
    ///     Add a user to a review body
    /// </summary>
    /// <param name="reviewBodyUser">Review Body User Dto</param>
    [HttpPost("adduser")]
    public async Task<ReviewBodyUserDto> AddUser(ReviewBodyUserDto reviewBodyUser)
    {
        var request = new AddReviewBodyUserCommand(reviewBodyUser);
        var adduser = await mediator.Send(request);

        return adduser;
    }

    /// <summary>
    ///     Get review body users by a list of ids
    /// </summary>
    /// <param name="ids">List of User Ids</param>
    [HttpPost("reviewbodyusersbyids")]
    public async Task<List<ReviewBodyUserDto>> GetUserReviewBodiesByIds([FromBody] List<Guid> ids)
    {
        var request = new GetReviewBodyUserByIdsCommand(ids);
        return await mediator.Send(request);
    }
}