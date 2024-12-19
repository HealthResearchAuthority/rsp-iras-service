using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class SaveResponsesHandler(IRespondentService respondentService) : IRequestHandler<SaveResponsesCommand>
{
    public async Task Handle(SaveResponsesCommand request, CancellationToken cancellationToken)
    {
        await respondentService.SaveResponses(request.RespondentAnswersRequest);
    }
}