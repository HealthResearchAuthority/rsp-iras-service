﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.Commands;
using Rsp.IrasService.Application.Queries;
using Rsp.IrasService.Application.Requests;
using Rsp.IrasService.Application.Responses;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationsController(IMediator mediator) : ControllerBase
{
    [HttpGet()]
    [Produces<IrasApplication>]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<GetApplicationResponse> GetApplication(int id)
    {
        var query = new GetApplicationQuery(id);

        return await mediator.Send(query);
    }

    [HttpGet("all")]
    [Produces<IEnumerable<IrasApplication>>]
    public async Task<IEnumerable<GetApplicationResponse>> GetApplications()
    {
        var query = new GetApplicationsQuery();

        return await mediator.Send(query);
    }

    [HttpPost()]
    public async Task<CreateApplicationResponse> CreateApplication(CreateApplicationRequest createApplicationRequest)
    {
        var request = new CreateApplicationCommand(createApplicationRequest);

        return await mediator.Send(request);
    }

    [HttpPost("update")]
    public async Task<CreateApplicationResponse> UpdateApplication(int id, CreateApplicationRequest irasApplicationRequest)
    {
        var request = new UpdateApplicationCommand(id, irasApplicationRequest);

        return await mediator.Send(request);
    }
}