using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class SendEmailCommand(SendEmailRequest request) : IRequest<SendEmailResponse>
{
    public SendEmailRequest SendEmailRequest { get; set; } = request;
}

