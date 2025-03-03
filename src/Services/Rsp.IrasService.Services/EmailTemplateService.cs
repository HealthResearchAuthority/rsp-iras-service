using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

public class EmailTemplateService(IEmailTemplateRepository repository) : IEmailTemplateService
{
    public async Task<EmailTemplate?> GetEmailTemplateForEventType(string eventTypeId)
    {
        var specification = new GetEmailTemplateSpecification(eventTypeId);

        return await repository.GetEmailTemplateForEventType(specification);
    }
}