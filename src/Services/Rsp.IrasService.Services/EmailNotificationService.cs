using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.CQRS.Queries;

namespace Rsp.IrasService.Services;

public class EmailNotificationService(IMediator mediator) : IEmailNotificationService
{
    public async Task<bool> SendEmail(SendEmailRequest requestData)
    {
        // get event type data
        var eventType = await mediator.Send(new GetEventTypeByIdQuery(requestData.EventTypeId));
        if (eventType != null)
        {
            // get email templates related to this event type
            var template = await mediator.Send(new GetEmailTemplateForEventQuery(requestData.EventTypeId));

            if (template != null)
            {
                var request = new EmailNotificationQueueRequest();
                var messages = new List<EmailNotificationQueueMessage>();

                // create email notification message for each email recipient
                foreach (var recipient in requestData.EmailRecipients)
                {
                    var message = new EmailNotificationQueueMessage
                    {
                        EmailTemplateId = template.TemplateId,
                        EventName = eventType.EventName,
                        EventType = eventType.Id,
                        RecipientAdress = recipient,
                        Data = requestData.Data
                    };

                    messages.Add(message);
                }
                request.NotificationMessages = messages;

                // send message to queue
                await mediator.Send(new EmailNotificationQueueCommand(request));

                return true;
            }
        }

        return false;
    }
}

