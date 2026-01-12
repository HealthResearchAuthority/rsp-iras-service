using MediatR;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Commands;

public class CreateProjectClosureCommand(ProjectClosureRequest createProjectClosureRequest) : IRequest<ProjectClosureResponse>
{
    public ProjectClosureRequest CreateProjectClosureRequest { get; set; } = createProjectClosureRequest;
}