using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class SaveModificationChangeCommand(ModificationChangeRequest request) : IRequest<ModificationChangeResponse>
{
    public ModificationChangeRequest ModificationChangeRequest { get; set; } = request;
}