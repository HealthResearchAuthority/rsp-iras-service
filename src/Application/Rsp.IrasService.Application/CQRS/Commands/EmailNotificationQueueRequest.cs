using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class EmailNotificationQueueCommand(EmailNotificationQueueRequest request) : IRequest
{
    public EmailNotificationQueueRequest EmailNotificationRequest { get; set; } = request;
}
