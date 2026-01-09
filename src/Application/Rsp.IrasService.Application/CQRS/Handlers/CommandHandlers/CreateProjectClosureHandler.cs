using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class CreateProjectClosureHandler(IProjectClosureService projectClosureService)
    : IRequestHandler<CreateProjectClosureCommand, ProjectClosureResponse>
{
    public async Task<ProjectClosureResponse> Handle(CreateProjectClosureCommand request, CancellationToken cancellationToken)
    {
        return await projectClosureService.CreateProjectClosure(request.CreateProjectClosureRequest);
    }
}