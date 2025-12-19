using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetProjectClosureHandler(IProjectClosureService projectClosureService) : IRequestHandler<GetProjectClosureQuery, ProjectClosureResponse>
{
    public async Task<ProjectClosureResponse> Handle(GetProjectClosureQuery request, CancellationToken cancellationToken)
    {
        return await projectClosureService.GetProjectClosure(request.ProjectRecordId);
    }
}