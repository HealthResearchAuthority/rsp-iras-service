using MediatR;

namespace Rsp.Service.Application.CQRS.Commands;

public class RemoveModificationChangeCommand(Guid modificationChangeId) : IRequest
{
    public Guid ModificationChangeId { get; set; } = modificationChangeId;
}