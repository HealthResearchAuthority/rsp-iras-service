using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class SaveModificationAnswersHandler(IRespondentService respondentService) : IRequestHandler<SaveModificationAnswersCommand>
{
    public async Task Handle(SaveModificationAnswersCommand request, CancellationToken cancellationToken)
    {
        await respondentService.SaveModificationAnswers(request.ModificationAnswersRequest);
    }
}