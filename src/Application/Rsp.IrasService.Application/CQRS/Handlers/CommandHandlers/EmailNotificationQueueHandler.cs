using MediatR;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers
{
    public class EmailNotificationQueueHandler(IEmailMessageQueueService mqService) : IRequestHandler<EmailNotificationQueueCommand>
    { 
        public async Task Handle(EmailNotificationQueueCommand request, CancellationToken cancellationToken)
        {
            await mqService.SendMessageToQueueAsync(request.EmailNotificationRequest.NotificationMessages);            
        }
    }
}
