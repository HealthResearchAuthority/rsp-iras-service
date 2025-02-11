using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories
{
    public class EmailNotificationRepository(IrasContext db) : IEmailNotificationRepositoy
    {
        public async Task<EventType> GetById(string eventId)
        {
            return await db.EventTypes.FindAsync(eventId);
        }

        public async Task<IEnumerable<EmailTemplate>> GetEmailTemplatesForEventType(string eventTypeId)
        {
            return await db
                .EmailTemplates
                .Where(x => x.EventTypeId == eventTypeId)
                .ToListAsync();
        }
    }
}
