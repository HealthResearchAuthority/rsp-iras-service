using MediatR;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class SaveResponsesHandler(ILogger<SaveResponsesHandler> logger, IRespondentService respondentService) : IRequestHandler<SaveResponsesCommand>
{
    public async Task Handle(SaveResponsesCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Saving respondent answers");

        await respondentService.SaveResponses(request.RespondentAnswersRequest);
    }
}