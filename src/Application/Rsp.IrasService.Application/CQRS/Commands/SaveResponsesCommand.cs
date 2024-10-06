using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class SaveResponsesCommand(RespondentAnswersRequest request) : IRequest
{
    public RespondentAnswersRequest RespondentAnswersRequest { get; set; } = request;
}