using MediatR;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.CQRS.Queries
{
    public class GetEventTypeByIdQuery(string id) : IRequest<EventType>
    {
        public string Id { get; } = id;
    }
}
