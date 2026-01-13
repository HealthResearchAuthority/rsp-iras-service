using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.Repositories;

public class EmailTemplateRepository(IrasContext db) : IEmailTemplateRepository
{
    public async Task<EmailTemplate?> GetEmailTemplateForEventType(ISpecification<EmailTemplate> specification)
    {
        return await db
            .EmailTemplates.WithSpecification(specification)
            .FirstOrDefaultAsync();
    }
}