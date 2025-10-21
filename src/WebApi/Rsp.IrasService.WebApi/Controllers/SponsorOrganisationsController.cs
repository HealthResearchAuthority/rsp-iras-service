using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class SponsorOrganisationsController(IMediator mediator) : ControllerBase
{
    [HttpPost("all")]
    [Produces<AllSponsorOrganisationsResponse>]
    public async Task<AllSponsorOrganisationsResponse> GetAllSponsorOrganisations(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        [FromQuery] string sortField,
        [FromQuery] string sortDirection,
        [FromBody] SponsorOrganisationSearchRequest searchQuery)
    {
        var query = new GetSponsorOrganisationsQuery(pageNumber, pageSize, sortField, sortDirection, searchQuery);
        return await mediator.Send(query);
    }

    [HttpGet("{rtsId}")]
    [Produces<AllSponsorOrganisationsResponse>]
    public async Task<AllSponsorOrganisationsResponse> GetSponsorOrganisationByRtsId(
        string rtsId)
    {
        var rtsIds = rtsId
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToList();

        var searchQuery = new SponsorOrganisationSearchRequest
        {
            RtsIds = rtsIds
        };

        var query = new GetSponsorOrganisationsQuery(1, int.MaxValue, "name", "asc", searchQuery);
        return await mediator.Send(query);
    }

    /// <summary>
    ///     Creates a review body
    /// </summary>
    /// <param name="reviewBodyDto">Research Body Dto</param>
    [HttpPost("create")]
    public async Task<SponsorOrganisationDto> Create
        (SponsorOrganisationDto sponsorOrganisationDto)
    {
        var request = new CreateSponsorOrganisationCommand(sponsorOrganisationDto);
        return await mediator.Send(request);
    }

    /// <summary>
    ///     Add a user to a Sponsor Organisation
    /// </summary>
    /// <param name="sponsorOrganisationUser">Sponsor Organisation User Dto</param>
    [HttpPost("adduser")]
    public async Task<SponsorOrganisationUserDto> AddUser(SponsorOrganisationUserDto sponsorOrganisationUser)
    {
        var request = new AddSponsorOrganisationUserCommand(sponsorOrganisationUser);
        return await mediator.Send(request);

    }

    /// <summary>
    ///     Gets a user in a Sponsor Organisation
    /// </summary>
    /// <param name="rtsId">Sponsor Organisation RTS Id</param>
    /// <param name="userId">Sponsor Organisation User Id</param>
    [HttpGet("{rtsId}/user/{userId}")]
    public async Task<ActionResult<SponsorOrganisationUserDto>> GetUser(
        string rtsId,
        Guid userId)
    {
        var request = new GetSponsorOrganisationUserCommand(rtsId, userId);
        return await mediator.Send(request);
    }

    /// <summary>
    ///     Gets a user in a Sponsor Organisation
    /// </summary>
    /// <param name="rtsId">Sponsor Organisation RTS Id</param>
    /// <param name="userId">Sponsor Organisation User Id</param>
    [HttpGet("{rtsId}/user/{userId}/enable")]
    public async Task<ActionResult<SponsorOrganisationUserDto>> EnableUser(
        string rtsId,
        Guid userId)
    {
        var request = new EnableSponsorOrganisationUserCommand(rtsId, userId);
        return await mediator.Send(request);
    }

    /// <summary>
    ///     Gets a user in a Sponsor Organisation
    /// </summary>
    /// <param name="rtsId">Sponsor Organisation RTS Id</param>
    /// <param name="userId">Sponsor Organisation User Id</param>
    [HttpGet("{rtsId}/user/{userId}/disable")]
    public async Task<ActionResult<SponsorOrganisationUserDto>> DisableUser(
        string rtsId,
        Guid userId)
    {
        var request = new DisableSponsorOrganisationUserCommand(rtsId, userId);
        return await mediator.Send(request);
    }

    /// <summary>
    ///     Gets a user in a Sponsor Organisation
    /// </summary>
    /// <param name="rtsId">Sponsor Organisation RTS Id</param>
    /// <param name="userId">Sponsor Organisation User Id</param>
    [HttpGet("{rtsId}/enable")]
    public async Task<ActionResult<SponsorOrganisationDto>> EnableSponsorOrganisation(
        string rtsId)
    {
        var request = new EnableSponsorOrganisationCommand(rtsId);
        return await mediator.Send(request);
    }

    /// <summary>
    ///     Gets a user in a Sponsor Organisation
    /// </summary>
    /// <param name="rtsId">Sponsor Organisation RTS Id</param>
    /// <param name="userId">Sponsor Organisation User Id</param>
    [HttpGet("{rtsId}/disable")]
    public async Task<ActionResult<SponsorOrganisationDto>> DisableSponsorOrganisation(
        string rtsId)
    {
        var request = new DisableSponsorOrganisationCommand(rtsId);
        return await mediator.Send(request);
    }
}

