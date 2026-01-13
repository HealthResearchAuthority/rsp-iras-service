using MediatR;

namespace Rsp.Service.Application.CQRS.Commands;

public class UpdateProjectRecordStatusCommand : IRequest
{
    public string ProjectRecordId { get; set; }
    public string Status { get; set; }
}