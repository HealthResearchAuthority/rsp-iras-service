using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class CreateModificationHandler(IProjectModificationService projectModificationService) : IRequestHandler<CreateModificationCommand, ModificationResponse>
{
    public async Task<ModificationResponse> Handle(CreateModificationCommand request, CancellationToken cancellationToken)
    {
        return await projectModificationService.CreateModification(request.ModificationRequest);
    }
}