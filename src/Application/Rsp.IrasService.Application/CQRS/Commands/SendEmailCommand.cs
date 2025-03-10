using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class SendEmailCommand(TriggerSendEmailRequest request) : IRequest<SendEmailResponse>
{
    public TriggerSendEmailRequest SendEmailRequest { get; set; } = request;
}