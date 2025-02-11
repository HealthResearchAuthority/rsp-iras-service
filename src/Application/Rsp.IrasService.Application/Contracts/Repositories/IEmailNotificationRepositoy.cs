using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories
{
    public interface IEmailNotificationRepositoy
    {
        Task<EventType> GetById(string eventId);

        Task<IEnumerable<EmailTemplate>> GetEmailTemplatesForEventType(string eventTypeId);
    }
}
