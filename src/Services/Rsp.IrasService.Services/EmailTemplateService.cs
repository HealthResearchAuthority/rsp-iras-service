using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services
{
    public class EmailTemplateService(IEmailTemplateRepository repository) : IEmailTemplateService
    {
        public async Task<EmailTemplate> GetEmailTemplateForEventType(string eventTypeId)
        {
            return await repository.GetEmailTemplateForEventType(eventTypeId);
        }
    }
}
