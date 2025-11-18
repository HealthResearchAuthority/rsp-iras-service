using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class UpdateModificationCommand() : IRequest
{
    public required UpdateModificationRequest ModificationRequest { get; set; }
}