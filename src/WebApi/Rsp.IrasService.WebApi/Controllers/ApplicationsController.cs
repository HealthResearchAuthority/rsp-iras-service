using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;

namespace Rsp.IrasService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationsController(ILogger<CategoriesController> logger, IApplicationsService applicationsService, IrasContext irasContext) : ControllerBase
{
    [HttpGet()]
    [Produces<IrasApplication>]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IrasApplication?> GetApplication(int id)
    {
        logger.LogInformation("Getting application with ID = {id}", id);

        return await irasContext.IrasApplications.FirstOrDefaultAsync(record => record.Id == id);
    }

    [HttpGet("all")]
    [Produces<IEnumerable<IrasApplication>>]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IEnumerable<IrasApplication>> GetApplications()
    {
        logger.LogInformation("Getting all applications");

        return irasContext.IrasApplications.AsEnumerable();
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IrasApplication> CreateApplication(IrasApplication irasApplication)
    {
        logger.LogInformation("Creating IRAS application");

        var entity = irasContext.IrasApplications.Add(irasApplication);
        await irasContext.SaveChangesAsync();

        return entity.Entity;
    }

    [HttpPost("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task UpdateApplication(int id, IrasApplication irasApplication)
    {
        logger.LogInformation("Update IRAS application with ID: {id}", id);

        var application = irasContext.IrasApplications.FirstOrDefault(record => record.Id == id);

        if (application != null)
        {
            application.Title = irasApplication.Title;
            application.Location = irasApplication.Location;
            application.StartDate = irasApplication.StartDate;
            application.ApplicationCategories = irasApplication.ApplicationCategories;
            application.ProjectCategory = irasApplication.ProjectCategory;

            await irasContext.SaveChangesAsync();
        }
    }
}