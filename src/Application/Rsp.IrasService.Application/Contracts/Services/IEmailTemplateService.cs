using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Services;

public interface IEmailTemplateService
{
    Task<EmailTemplate?> GetEmailTemplateForEventType(string eventTypeId);
}