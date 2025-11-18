using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationHandler(IProjectModificationService projectModificationService) : IRequestHandler<GetModificationQuery, ModificationResponse?>
{
    public async Task<ModificationResponse?> Handle(GetModificationQuery request, CancellationToken cancellationToken)
    {
        return await projectModificationService.GetModification(request.ProjectModificationId);
    }
}