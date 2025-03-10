using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IEmailTemplateRepository
{
    Task<EmailTemplate?> GetEmailTemplateForEventType(ISpecification<EmailTemplate> specification);
}