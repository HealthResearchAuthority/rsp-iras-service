using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class UpdateModificationChangeCommand() : IRequest
{
    public required UpdateModificationChangeRequest ModificationChangeRequest { get; set; }
}