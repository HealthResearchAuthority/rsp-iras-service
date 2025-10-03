using MediatR;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class DeleteModificationCommand(Guid modificationId) : IRequest
{
    public Guid ProjectModificationId { get; set; } = modificationId;
}