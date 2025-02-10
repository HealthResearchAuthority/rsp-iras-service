using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Rsp.IrasService.Domain.Entities.Notifications;

namespace Rsp.IrasService.Application.CQRS.Commands
{
    public class SendEmailNotificationCommand(EmailNotificationRequest request) : IRequest
    {
        public EmailNotificationRequest EmailNotificationRequest { get; set; } = request;
    }
}
