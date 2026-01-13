using MediatR;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class UpdateProjectRecordStatusCommand : IRequest
{
    public string ProjectRecordId { get; set; }
    public string Status { get; set; }
}