using MediatR;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class RemoveModificationChangeCommand(Guid modificationChangeId) : IRequest
{
    public Guid ModificationChangeId { get; set; } = modificationChangeId;
}