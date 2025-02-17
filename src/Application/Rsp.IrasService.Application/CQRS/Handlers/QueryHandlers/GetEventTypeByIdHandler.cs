using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
public class GetEventTypeByIdHandler(IEventTypeService service) : IRequestHandler<GetEventTypeByIdQuery, EventType>
{
    public Task<EventType> Handle(GetEventTypeByIdQuery request, CancellationToken cancellationToken)
    {
        return service.GetById(request.Id);
    }
}
