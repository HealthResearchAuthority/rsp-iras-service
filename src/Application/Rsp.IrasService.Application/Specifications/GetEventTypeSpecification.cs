﻿using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications
{
    public class GetEventTypeSpecification : Specification<EventType>
    {

        public GetEventTypeSpecification(int eventTypeId)
        {
            Query.AsNoTracking()
                .Where(x => x.Id == eventTypeId);
        }
    }
}
