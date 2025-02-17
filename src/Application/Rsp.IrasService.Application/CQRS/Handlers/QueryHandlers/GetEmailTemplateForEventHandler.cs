using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers
{
    public class GetEmailTemplateForEventHandler(IEmailTemplateService service) : IRequestHandler<GetEmailTemplateForEventQuery, EmailTemplate>
    {
        public Task<EmailTemplate> Handle(GetEmailTemplateForEventQuery request, CancellationToken cancellationToken)
        {
            return service.GetEmailTemplateForEventType(request.EventId);
        }
    }
}
