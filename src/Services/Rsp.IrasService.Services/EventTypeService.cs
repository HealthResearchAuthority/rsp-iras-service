using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;
public class EventTypeService(IEventTypeRepository repository) : IEventTypeService
{
    public async Task<EventType> GetById(string eventId)
    {
        return await repository.GetById(eventId);
    }
}
