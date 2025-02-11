using MediatR;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.CQRS.Queries
{
    public class GetEmailTemplatesForEventQuery(string eventId) : IRequest<IEnumerable<EmailTemplate>>
    {
        public string EventId { get; } = eventId;
    }
}
