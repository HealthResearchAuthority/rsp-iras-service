using MediatR;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.CQRS.Queries
{
    public class GetEmailTemplateForEventQuery(string eventId) : IRequest<EmailTemplate>
    {
        public string EventId { get; } = eventId;
    }
}
