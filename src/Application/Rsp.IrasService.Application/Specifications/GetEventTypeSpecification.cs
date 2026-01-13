using Ardalis.Specification;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Specifications;

public class GetEventTypeSpecification : Specification<EventType>
{
    public GetEventTypeSpecification(string eventTypeId)
    {
        Query.AsNoTracking()
            .Where(x => x.Id == eventTypeId);
    }
}