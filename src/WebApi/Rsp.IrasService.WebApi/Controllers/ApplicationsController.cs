using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Application.DTOs;
using Rsp.IrasService.Application.Responses;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;

namespace Rsp.IrasService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationsController(ILogger<ApplicationsController> logger, IApplicationsService applicationsService) : ControllerBase
{
    [HttpGet()]
    [Produces<IrasApplication>]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<GetApplicationResponse> GetApplication(int id)
    {
        logger.LogInformation("Getting application with ID = {id}", id);

        return await applicationsService.GetApplication(id);
    }

    [HttpGet("all")]
    [Produces<IEnumerable<IrasApplication>>]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetApplicationResponse>> GetApplications()
    {
        logger.LogInformation("Getting all applications");

        return await applicationsService.GetApplications();
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<CreateApplicationResponse> CreateApplication(CreateApplicationRequest irasApplicationRequest)
    {
        logger.LogInformation("Creating IRAS application");

        return await applicationsService.CreateApplication(irasApplicationRequest);
    }

    [HttpPost("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<CreateApplicationResponse> UpdateApplication(int id, CreateApplicationRequest irasApplicationRequest)
    {
        logger.LogInformation("Update IRAS application with ID: {id}", id);

        return await applicationsService.UpdateApplication(id, irasApplicationRequest);
    }
}