using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Contracts;

namespace Rsp.IrasService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CategoriesController(ILogger<CategoriesController> logger, ICategoriesService categoriesService) : ControllerBase
{
    /// <summary>
    /// Returns the list of application categories
    /// </summary>
    [HttpGet("apps")]
    [Produces<IEnumerable<string>>]
    public async Task<IEnumerable<string>> GetApplicationCategories()
    {
        logger.LogInformation("Getting Application Categories");

        return await categoriesService.GetApplicationCategories();
    }

    /// <summary>
    /// Adds an application category to the list
    /// </summary>
    /// <param name="category">Application Category Name</param>
    [HttpPost("apps")]
    public async Task AddApplicationCategory(string category)
    {
        logger.LogInformation("Adding Application Category");

        await categoriesService.AddApplicationCategory(category);
    }

    /// <summary>
    /// Returns the list of project categories
    /// </summary>
    [HttpGet("projects")]
    [Produces<IEnumerable<string>>]
    public async Task<IEnumerable<string>> GetProjectCategories()
    {
        logger.LogInformation("Getting Project Categories");

        return await categoriesService.GetProjectCategories();
    }

    /// <summary>
    /// Adds a project category to the list
    /// </summary>
    /// <param name="category">Project Category Name</param>
    [HttpPost("projects")]
    public async Task AddProjectCategory(string category)
    {
        logger.LogInformation("Adding Project Category");

        await categoriesService.AddProjectCategory(category);
    }
}