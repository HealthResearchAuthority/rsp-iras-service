using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class UpdateModificationCommand() : IRequest
{
    public required UpdateModificationRequest ModificationRequest { get; set; }
}