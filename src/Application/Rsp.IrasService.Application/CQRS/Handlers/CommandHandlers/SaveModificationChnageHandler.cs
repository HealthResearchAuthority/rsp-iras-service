using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class SaveModificationChangeHandler(IProjectModificationService projectModificationService) : IRequestHandler<SaveModificationChangeCommand, ModificationChangeResponse>
{
    public async Task<ModificationChangeResponse> Handle(SaveModificationChangeCommand request, CancellationToken cancellationToken)
    {
        return await projectModificationService.CreateOrUpdateModificationChange(request.ModificationChangeRequest);
    }
}