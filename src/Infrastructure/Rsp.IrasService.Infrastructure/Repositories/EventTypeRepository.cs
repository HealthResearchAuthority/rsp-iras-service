using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories
{
    public class EventTypeRepository(IrasContext db) : IEventTypeRepository
    {
        public async Task<EventType> GetById(string eventId)
        {
            return await db.EventTypes.FindAsync(eventId);
        }
    }
}
