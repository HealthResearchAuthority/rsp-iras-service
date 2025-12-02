using MediatR;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class UpdateModificationStatusCommand : IRequest
{
    public required string ProjectRecordId { get; set; }
    public required Guid ProjectModificationId { get; set; }
    public required string Status { get; set; }
}