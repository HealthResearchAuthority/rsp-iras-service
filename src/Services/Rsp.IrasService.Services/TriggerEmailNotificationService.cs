using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Services;

public class TriggerEmailNotificationService(
    IEmailTemplateService templateService,
    IMessageQueueService mqService) : ITriggerEmailNotificationService
{
    public async Task<bool> TriggerSendEmail(TriggerSendEmailRequest requestData)
    {
        // get email templates related to this event type
        var template = await templateService.GetEmailTemplateForEventType(requestData.EventTypeId);

        if (template != null)
        {
            //var request = new EmailNotificationQueueRequest();
            var messages = new List<EmailNotificationQueueMessage>();

            // create email notification message for each email recipient
            foreach (var recipient in requestData.EmailRecipients)
            {
                var message = new EmailNotificationQueueMessage
                {
                    EmailTemplateId = template.TemplateId,
                    EventName = template.EventType?.EventName,
                    EventType = template.EventType?.Id,
                    RecipientAdress = recipient,
                    Data = requestData.Data
                };

                messages.Add(message);
            }

            // send message to queue
            await mqService.SendMessageToQueueAsync(messages);

            return true;
        }

        return false;
    }
}