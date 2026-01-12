using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class CreateProjectClosureHandler(IProjectClosureService projectClosureService)
    : IRequestHandler<CreateProjectClosureCommand, ProjectClosureResponse>
{
    public async Task<ProjectClosureResponse> Handle(CreateProjectClosureCommand request, CancellationToken cancellationToken)
    {
        return await projectClosureService.CreateProjectClosure(request.CreateProjectClosureRequest);
    }
}