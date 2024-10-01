using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.Authorization.Attributes;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Produces<ResearchApplication>]
    public async Task<ApplicationResponse> GetApplication(string applicationId)
    {
        var query = new GetApplicationQuery(applicationId);

        return await mediator.Send(query);
    }

    [HttpGet("all")]
    [Produces<IEnumerable<ResearchApplication>>]
    public async Task<IEnumerable<ApplicationResponse>> GetApplications()
    {
        var query = new GetApplicationsQuery();

        return await mediator.Send(query);
    }

    [HttpPost]
    public async Task<ApplicationResponse> CreateApplication(ApplicationRequest applicationRequest)
    {
        var request = new CreateApplicationCommand(applicationRequest);

        return await mediator.Send(request);
    }

    [HttpPost("update")]
    public async Task<ApplicationResponse> UpdateApplication(ApplicationRequest applicationRequest)
    {
        var request = new UpdateApplicationCommand(applicationRequest);

        return await mediator.Send(request);
    }

    [HttpGet("{status}")]
    [ApplicationAccess]
    public async Task<ApplicationResponse> GetApplicationByStatus(string applicationId, string status)
    {
        var request = new GetApplicationWithStatusQuery(applicationId)
        {
            ApplicationStatus = status
        };

        return await mediator.Send(request);
    }

    [HttpGet("{status}/all")]
    [ApplicationAccess]
    public async Task<IEnumerable<ApplicationResponse>> GetApplicationsByStatus(string status)
    {
        var request = new GetApplicationsWithStatusQuery
        {
            ApplicationStatus = status
        };

        return await mediator.Send(request);
    }
}