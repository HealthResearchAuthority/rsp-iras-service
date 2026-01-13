using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Contracts.Services;

public interface IEmailTemplateService
{
    Task<EmailTemplate?> GetEmailTemplateForEventType(string eventTypeId);
}