using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class CreateProjectClosureCommand(ProjectClosureRequest createProjectClosureRequest) : IRequest<ProjectClosureResponse>
{
    public ProjectClosureRequest CreateProjectClosureRequest { get; set; } = createProjectClosureRequest;
}