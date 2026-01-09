using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetProjectClosureHandler(IProjectClosureService projectClosureService) : IRequestHandler<GetProjectClosureQuery, ProjectClosuresSearchResponse>
{
    public async Task<ProjectClosuresSearchResponse> Handle(GetProjectClosureQuery request, CancellationToken cancellationToken)
    {
        return await projectClosureService.GetProjectClosuresByProjectRecordId(request.ProjectRecordId);
    }
}