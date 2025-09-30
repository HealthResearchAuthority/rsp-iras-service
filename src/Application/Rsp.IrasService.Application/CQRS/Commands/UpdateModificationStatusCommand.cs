using MediatR;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class UpdateModificationStatusCommand(Guid modificationId, string status) : IRequest
{
    public Guid ProjectModificationId { get; set; } = modificationId;

    public string Status { get; set; } = status;
}