using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.Constants;
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
    [HttpPost("testqueue")]
    public async Task TestQueue(EmailNotificationQueueMessage message)
    {
        var sendEmailRequestData = new TriggerSendEmailRequest
        {
            EventTypeId = EventTypeAlias.ApplicationCreated,
            EmailRecipients = new List<string> { message.RecipientAdress },
            Data = message.Data
        };

        var sendEmail = await mediator.Send(new SendEmailCommand(sendEmailRequestData));
    }

    /// <summary>
    /// Returns a single application
    /// </summary>
    /// <param name="applicationId">Research Application Id</param>
    [HttpGet("{applicationId}")]
    [Produces<ResearchApplication>]
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
    [Produces<ResearchApplication>]
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
    [Produces<IEnumerable<ResearchApplication>>]
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
    [Produces<IEnumerable<ResearchApplication>>]
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
    [Produces<IEnumerable<ResearchApplication>>]
    public async Task<IEnumerable<ApplicationResponse>> GetApplicationsByRespondent(string respondentId)
    {
        var request = new GetApplicationsWithRespondentQuery
        {
            RespondentId = respondentId
        };

        return await mediator.Send(request);
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