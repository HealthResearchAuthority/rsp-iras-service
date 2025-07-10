using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class CreateModificationCommand(ModificationRequest modificationRequest) : IRequest<ModificationResponse>
{
    public ModificationRequest ModificationRequest { get; set; } = modificationRequest;
}