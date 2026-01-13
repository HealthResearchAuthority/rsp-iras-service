using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetProjectClosureHandler(IProjectClosureService projectClosureService) : IRequestHandler<GetProjectClosureQuery, ProjectClosuresSearchResponse>
{
    public async Task<ProjectClosuresSearchResponse> Handle(GetProjectClosureQuery request, CancellationToken cancellationToken)
    {
        return await projectClosureService.GetProjectClosuresByProjectRecordId(request.ProjectRecordId);
    }
}