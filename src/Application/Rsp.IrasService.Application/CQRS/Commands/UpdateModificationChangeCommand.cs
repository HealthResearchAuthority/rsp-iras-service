using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class UpdateModificationChangeCommand() : IRequest
{
    public required UpdateModificationChangeRequest ModificationChangeRequest { get; set; }
}