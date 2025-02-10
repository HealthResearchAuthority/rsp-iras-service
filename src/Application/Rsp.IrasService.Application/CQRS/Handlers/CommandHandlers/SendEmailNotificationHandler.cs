using MediatR;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers
{
    public class SendEmailNotificationHandler(IEmailMessageQueueService mqService) : IRequestHandler<SendEmailNotificationCommand>
    { 
        public async Task Handle(SendEmailNotificationCommand request, CancellationToken cancellationToken)
        {
            await mqService.SendMessageAsync(request.EmailNotificationRequest);            
        }
    }
}
