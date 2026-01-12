using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class SaveResponsesCommand(RespondentAnswersRequest request) : IRequest
{
    public RespondentAnswersRequest RespondentAnswersRequest { get; set; } = request;
}