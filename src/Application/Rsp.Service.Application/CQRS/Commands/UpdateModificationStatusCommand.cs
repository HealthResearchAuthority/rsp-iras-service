using MediatR;

namespace Rsp.Service.Application.CQRS.Commands;

public class UpdateModificationStatusCommand : IRequest
{
    public required string ProjectRecordId { get; set; }
    public required Guid ProjectModificationId { get; set; }
    public required string Status { get; set; }
}