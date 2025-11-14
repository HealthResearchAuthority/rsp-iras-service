using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class SaveModificationReviewResponsesHandler(IProjectModificationService projectModificationService) :
    IRequestHandler<SaveModificationReviewResponsesCommand>
{
    public Task Handle(SaveModificationReviewResponsesCommand request, CancellationToken cancellationToken)
    {
        return projectModificationService.SaveModificationReviewResponses(request.ModificationReviewRequest);
    }
}