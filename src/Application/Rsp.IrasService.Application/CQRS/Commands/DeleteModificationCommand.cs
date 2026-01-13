using MediatR;

namespace Rsp.Service.Application.CQRS.Commands;

public class DeleteModificationCommand : IRequest
{
    public required string ProjectRecordId { get; set; }
    public required Guid ProjectModificationId { get; set; }
}