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

}