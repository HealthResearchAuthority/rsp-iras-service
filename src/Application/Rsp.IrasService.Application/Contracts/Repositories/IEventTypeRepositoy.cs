using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IEventTypeRepository
{
    Task<EventType?> GetById(ISpecification<EventType> specification);
}