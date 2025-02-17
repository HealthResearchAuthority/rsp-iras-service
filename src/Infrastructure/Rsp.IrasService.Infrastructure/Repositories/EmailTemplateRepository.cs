using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class EmailTemplateRepository(IrasContext db) : IEmailTemplateRepository
{
    public async Task<EmailTemplate?> GetEmailTemplateForEventType(ISpecification<EmailTemplate> specification)
    {
        return await db
            .EmailTemplates.WithSpecification(specification)
            .FirstOrDefaultAsync();
    }
}