using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services
{
    public class EmailNotificationService(IEmailNotificationRepositoy repository) : IEmailNotificationService
    {
        public Task<EventType> GetById(string eventId)
        {
            return repository.GetById(eventId);
        }

        public Task<IEnumerable<EmailTemplate>> GetEmailTemplatesForEventType(string eventTypeId)
        {
            return repository.GetEmailTemplatesForEventType(eventTypeId);
        }
    }
}
