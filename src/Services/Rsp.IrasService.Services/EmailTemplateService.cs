using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Services;

public class EmailTemplateService(IEmailTemplateRepository repository) : IEmailTemplateService
{
    public async Task<EmailTemplate?> GetEmailTemplateForEventType(string eventTypeId)
    {
        var specification = new GetEmailTemplateSpecification(eventTypeId);

        return await repository.GetEmailTemplateForEventType(specification);
    }
}