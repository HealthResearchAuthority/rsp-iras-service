using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Contracts.Services;

public interface IEventTypeService
{
    Task<EventType?> GetById(string eventId);
}