using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories
{
    public interface IEventTypeRepository
    {
        Task<EventType> GetById(string eventId);
    }
}
