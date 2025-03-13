using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewBodyController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     Returns all review bodies
    /// </summary>
    [HttpGet("all")]
    [Produces<IEnumerable<ReviewBody>>]
    public async Task<IEnumerable<ReviewBodyDto>> GetReviewBodies()
    {
        var query = new GetReviewBodiesQuery();

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
        return await mediator.Send(request);
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
        return await mediator.Send(request);
    }

    /// <summary>
    ///     Disable a review body
    /// </summary>
    /// <param name="reviewBodyDto">Research Body Dto</param>
    [HttpPut("disable/{id}")]
    public async Task<ReviewBodyDto?> Disable(Guid id)
    {
        var request = new DisableReviewBodyCommand(id);
        return await mediator.Send(request);
    }
}