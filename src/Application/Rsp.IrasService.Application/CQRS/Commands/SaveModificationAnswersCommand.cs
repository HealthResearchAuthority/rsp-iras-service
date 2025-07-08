using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class SaveModificationAnswersCommand(ModificationAnswersRequest request) : IRequest
{
    public ModificationAnswersRequest ModificationAnswersRequest { get; set; } = request;
}