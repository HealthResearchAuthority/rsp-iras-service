using MediatR;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class UpdateProjectClosureStatusCommand : IRequest
{
    public string ProjectRecordId { get; set; }
    public string Status { get; set; }
    public string UserId { get; set; }
}