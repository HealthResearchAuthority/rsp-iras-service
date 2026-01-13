using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.Repositories;

public class EventTypeRepository(IrasContext db) : IEventTypeRepository
{
    public async Task<EventType?> GetById(ISpecification<EventType> specification)
    {
        return await db.EventTypes
            .WithSpecification(specification)
            .FirstOrDefaultAsync();
    }
}