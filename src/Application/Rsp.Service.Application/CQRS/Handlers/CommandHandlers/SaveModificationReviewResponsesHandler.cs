using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class SaveModificationReviewResponsesHandler(IProjectModificationService projectModificationService) :
    IRequestHandler<SaveModificationReviewResponsesCommand>
{
    public Task Handle(SaveModificationReviewResponsesCommand request, CancellationToken cancellationToken)
    {
        return projectModificationService.SaveModificationReviewResponses(request.ModificationReviewRequest);
    }
}