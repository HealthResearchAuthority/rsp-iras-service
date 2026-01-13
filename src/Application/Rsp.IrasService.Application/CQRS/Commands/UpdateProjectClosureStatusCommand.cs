using MediatR;

namespace Rsp.Service.Application.CQRS.Commands;

public class UpdateProjectClosureStatusCommand : IRequest
{
    public string ProjectRecordId { get; set; }
    public string Status { get; set; }
    public string UserId { get; set; }
}