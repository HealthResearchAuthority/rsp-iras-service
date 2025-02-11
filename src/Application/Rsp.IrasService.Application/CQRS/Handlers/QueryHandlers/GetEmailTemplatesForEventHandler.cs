using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers
{
    public class GetEmailTemplatesForEventHandler(IEmailNotificationService service) : IRequestHandler<GetEmailTemplatesForEventQuery, IEnumerable<EmailTemplate>>
    {
        public Task<IEnumerable<EmailTemplate>> Handle(GetEmailTemplatesForEventQuery request, CancellationToken cancellationToken)
        {
            return service.GetEmailTemplatesForEventType(request.EventId);
        }
    }
}
