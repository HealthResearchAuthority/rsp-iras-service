using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

public class EventTypeService(IEventTypeRepository repository) : IEventTypeService
{
    public async Task<EventType?> GetById(string eventId)
    {
        var specification = new GetEventTypeSpecification(eventId);
        return await repository.GetById(specification);
    }
}