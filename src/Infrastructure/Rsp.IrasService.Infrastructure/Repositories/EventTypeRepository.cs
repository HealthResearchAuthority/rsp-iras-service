using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class EventTypeRepository(IrasContext db) : IEventTypeRepository
{
    public async Task<EventType?> GetById(ISpecification<EventType> specification)
    {
        return await db.EventTypes
            .WithSpecification(specification)
            .FirstOrDefaultAsync();
    }
}