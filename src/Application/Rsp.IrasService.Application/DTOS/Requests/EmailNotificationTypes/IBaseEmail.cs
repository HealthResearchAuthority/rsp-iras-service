namespace Rsp.IrasService.Application.DTOS.Requests.EmailNotificationTypes;

public interface IBaseEmail
{
    string EmailTemplateId { get; }
    string EventType { get; }
    DateTime OccurredAt { get; }
}