using MediatR;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Commands;

public class SendEmailCommand(TriggerSendEmailRequest request) : IRequest<SendEmailResponse>
{
    public TriggerSendEmailRequest SendEmailRequest { get; set; } = request;
}