using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ApplicationsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Returns a single application
    /// </summary>
    /// <param name="applicationId">Research Application Id</param>
    [HttpGet("{applicationId}")]
    [Produces<ProjectRecord>]
    public async Task<ActionResult<ApplicationResponse>> GetApplication(string applicationId)
    {
        var request = new GetApplicationQuery(applicationId);

        var response = await mediator.Send(request);

        return response == null ? NotFound() : Ok(response);
    }

    /// <summary>
    /// Returns a single application
    /// </summary>
    /// <param name="applicationId">Research Application Id</param>
    /// <param name="status">Research Application Status e.g. pending, created, approved</param>
    [HttpGet("{applicationId}/{status}")]
    [Produces<ProjectRecord>]
    public async Task<ActionResult<ApplicationResponse>> GetApplication(string applicationId, string status)
    {
        var request = new GetApplicationWithStatusQuery(applicationId)
        {
            ApplicationStatus = status
        };

        var response = await mediator.Send(request);

        return response == null ? NotFound() : Ok(response);
    }

    /// <summary>
    /// Returns all applications or applications by status
    /// </summary>
    [HttpGet]
    [Produces<IEnumerable<ProjectRecord>>]
    public async Task<IEnumerable<ApplicationResponse>> GetApplications()
    {
        var query = new GetApplicationsQuery();

        return await mediator.Send(query);
    }

    /// <summary>
    /// Returns all applications by status
    /// </summary>
    /// <param name="status">Research Application Status</param>
    [HttpGet("status")]
    [Produces<IEnumerable<ProjectRecord>>]
    public async Task<IEnumerable<ApplicationResponse>> GetApplicationsByStatus(string status)
    {
        var query = new GetApplicationsWithStatusQuery
        {
            ApplicationStatus = status
        };

        return await mediator.Send(query);
    }

    /// <summary>
    /// Returns all applications for the respondent
    /// </summary>
    /// <param name="respondentId">Reasearch Application Respondent Id</param>
    [HttpGet("respondent")]
    [Produces<IEnumerable<ProjectRecord>>]
    public async Task<IEnumerable<ApplicationResponse>> GetApplicationsByRespondent(string respondentId)
    {
        var request = new GetApplicationsWithRespondentQuery
        {
            RespondentId = respondentId,
        };

        return await mediator.Send(request);
    }

    /// <summary>
    /// Returns applications for the respondent, with possibility of pagination if pageIndex and pageSize are defined > 0 (defaut - no pagination)
    /// </summary>
    /// <param name="respondentId">Reasearch Application Respondent Id</param>
    /// <param name="searchQuery">Optional search query to filter projects by title or description.</param>
    /// <param name="pageIndex">Page number (1-based). Pagination applied if greater than 0.</param>
    /// <param name="pageSize">Number of records per page. Pagination applied if greater than 0.</param>
    [HttpGet("respondent/paginated")]
    [Produces(typeof(PaginatedResponse<ApplicationResponse>))]
    public async Task<PaginatedResponse<ApplicationResponse>> GetPaginatedApplicationsByRespondent(
        string respondentId,
        string? searchQuery = null,
        int pageIndex = 0,
        int pageSize = 0)
    {
        var request = new GetPaginatedApplicationsWithRespondentQuery
        {
            RespondentId = respondentId,
            SearchQuery = searchQuery,
            PageIndex = pageIndex,
            PageSize = pageSize,
        };

        var result = await mediator.Send(request);

        return new PaginatedResponse<ApplicationResponse>
        {
            Items = result.Items,
            TotalCount = result.TotalCount
        };
    }

    /// <summary>
    /// Creates a research application
    /// </summary>
    /// <param name="applicationRequest">Research Application Request</param>
    [HttpPost]
    public async Task<ApplicationResponse> CreateApplication(ApplicationRequest applicationRequest)
    {
        var request = new CreateApplicationCommand(applicationRequest);
        var newApplication = await mediator.Send(request);

        return newApplication;
    }

    /// <summary>
    /// Updates a research application
    /// </summary>
    /// <param name="applicationRequest">Research Application Request</param>
    [HttpPut]
    public async Task<ApplicationResponse> UpdateApplication(ApplicationRequest applicationRequest)
    {
        var request = new UpdateApplicationCommand(applicationRequest);

        return await mediator.Send(request);
    }
}