using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.WebApi.Controllers;

/// <summary>
/// Controller for handling respondent answers and modification answers.
/// </summary>
[ApiController]
[Route("[controller]")]
public class RespondentController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Saves respondent answers for a project record.
    /// </summary>
    /// <param name="request">The respondent answers request.</param>
    [HttpPost]
    public async Task SaveRespondentAnswers(RespondentAnswersRequest request)
    {
        var command = new SaveResponsesCommand(request);

        await mediator.Send(command);
    }

    /// <summary>
    /// Saves modification answers for a project modification change.
    /// </summary>
    /// <param name="request">The modification answers request.</param>
    [HttpPost("modification")]
    public async Task SaveModificationAnswers(ModificationAnswersRequest request)
    {
        var command = new SaveModificationAnswersCommand(request);

        await mediator.Send(command);
    }

    /// <summary>
    /// Gets respondent answers for a specific application.
    /// </summary>
    /// <param name="applicationId">The application identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    [HttpGet("{applicationId}")]
    [Produces<IEnumerable<RespondentAnswerDto>>]
    public async Task<IEnumerable<RespondentAnswerDto>> GetRespondentAnswers(string applicationId)
    {
        var command = new GetResponsesQuery
        {
            ApplicationId = applicationId
        };

        return await mediator.Send(command);
    }

    /// <summary>
    /// Gets respondent answers for a specific application and category.
    /// </summary>
    /// <param name="applicationId">The application identifier.</param>
    /// <param name="categoryId">The category identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    [HttpGet("{applicationId}/{categoryId}")]
    [Produces<IEnumerable<RespondentAnswerDto>>]
    public async Task<IEnumerable<RespondentAnswerDto>> GetRespondentAnswers(string applicationId, string categoryId)
    {
        var command = new GetResponsesQuery
        {
            ApplicationId = applicationId,
            CategoryId = categoryId
        };

        return await mediator.Send(command);
    }

    /// <summary>
    /// Gets modification answers for a specific modification change and project record.
    /// </summary>
    /// <param name="modificationChangeId">The modification change identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <returns>A collection of respondent answers for the modification.</returns>
    [HttpGet("modification/{modificationChangeId}/{projectRecordId}")]
    [Produces<IEnumerable<RespondentAnswerDto>>]
    public async Task<IEnumerable<RespondentAnswerDto>> GetModificationAnswers(Guid modificationChangeId, string projectRecordId)
    {
        var command = new GetModificationAnswersQuery
        {
            ProjectModificationChangeId = modificationChangeId,
            ProjectRecordId = projectRecordId
        };

        return await mediator.Send(command);
    }

    /// <summary>
    /// Gets modification answers for a specific modification change, project record, and category.
    /// </summary>
    /// <param name="modificationChangeId">The modification change identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <param name="categoryId">The category identifier.</param>
    /// <returns>A collection of respondent answers for the modification and category.</returns>
    [HttpGet("modification/{modificationChangeId}/{projectRecordId}/{categoryId}")]
    [Produces<IEnumerable<RespondentAnswerDto>>]
    public async Task<IEnumerable<RespondentAnswerDto>> GetModificationAnswers(Guid modificationChangeId, string projectRecordId, string categoryId)
    {
        var command = new GetModificationAnswersQuery
        {
            ProjectModificationChangeId = modificationChangeId,
            ProjectRecordId = projectRecordId,
            CategoryId = categoryId
        };

        return await mediator.Send(command);
    }
}