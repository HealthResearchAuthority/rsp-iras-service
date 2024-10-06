using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RespondentController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task SaveRespondentAnswers(RespondentAnswersRequest request)
    {
        var command = new SaveResponsesCommand(request);

        await mediator.Send(command);
    }

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
}