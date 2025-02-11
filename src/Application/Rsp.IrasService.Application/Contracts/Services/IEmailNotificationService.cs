using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Services
{
    public interface IEmailNotificationService
    {
        Task<EventType> GetById(string eventId);

        Task<IEnumerable<EmailTemplate>> GetEmailTemplatesForEventType(string eventTypeId);
    }
}
