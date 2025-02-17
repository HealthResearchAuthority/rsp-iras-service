
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Services;
public interface IEventTypeService
{
    Task<EventType> GetById(string eventId);
}
