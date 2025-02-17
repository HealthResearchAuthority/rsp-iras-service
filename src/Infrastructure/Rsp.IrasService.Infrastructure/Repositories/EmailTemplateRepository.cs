using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories
{
    public class EmailTemplateRepository(IrasContext db) : IEmailTemplateRepository
    {
        public async Task<EmailTemplate> GetEmailTemplateForEventType(string eventTypeId)
        {
            return await db
                .EmailTemplates
                .FirstOrDefaultAsync(x => x.EventTypeId == eventTypeId);
        }
    }
}
