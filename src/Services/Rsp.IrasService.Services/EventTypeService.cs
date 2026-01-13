using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Services;

public class EventTypeService(IEventTypeRepository repository) : IEventTypeService
{
    public async Task<EventType?> GetById(string eventId)
    {
        var specification = new GetEventTypeSpecification(eventId);
        return await repository.GetById(specification);
    }
}