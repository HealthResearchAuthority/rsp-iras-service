using MediatR;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class DeleteModificationCommand : IRequest
{
    public required string ProjectRecordId { get; set; }
    public required Guid ProjectModificationId { get; set; }
}