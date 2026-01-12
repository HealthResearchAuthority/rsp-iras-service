using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class SaveResponsesHandler(IRespondentService respondentService) : IRequestHandler<SaveResponsesCommand>
{
    public async Task Handle(SaveResponsesCommand request, CancellationToken cancellationToken)
    {
        await respondentService.SaveResponses(request.RespondentAnswersRequest);
    }
}