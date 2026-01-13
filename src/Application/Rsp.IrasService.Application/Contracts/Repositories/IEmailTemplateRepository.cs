using Ardalis.Specification;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Contracts.Repositories;

public interface IEmailTemplateRepository
{
    Task<EmailTemplate?> GetEmailTemplateForEventType(ISpecification<EmailTemplate> specification);
}