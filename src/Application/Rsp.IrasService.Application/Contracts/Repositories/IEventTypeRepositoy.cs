using Ardalis.Specification;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Contracts.Repositories;

public interface IEventTypeRepository
{
    Task<EventType?> GetById(ISpecification<EventType> specification);
}