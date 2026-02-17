namespace Rsp.IrasService.Application.DTOS.Requests.EmailNotificationTypes;

public record GenericEmail(
    string EmailTemplateId,
    string EventType,
    DateTime OccurredAt,
    GenericEmailData Data) : IBaseEmail;