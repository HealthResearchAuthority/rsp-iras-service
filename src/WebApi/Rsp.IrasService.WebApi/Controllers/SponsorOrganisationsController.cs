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
        var adduser = await mediator.Send(request);

        return adduser;
    }
}