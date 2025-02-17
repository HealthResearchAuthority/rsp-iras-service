
namespace Rsp.IrasService.Application.DTOS.Requests;
public class EmailNotificationQueueRequest
{
    public IEnumerable<EmailNotificationQueueMessage> NotificationMessages { get; set; }
}

