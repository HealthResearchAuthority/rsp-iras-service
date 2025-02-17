using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers
{
    public class SendEmailCommandHandler(IEmailNotificationService service) : IRequestHandler<SendEmailCommand, SendEmailResponse>
    {
        public async Task<SendEmailResponse> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var sendEmail = await service.SendEmail(request.SendEmailRequest);

            return new SendEmailResponse
            {
                IsSuccess = sendEmail
            };
        }
    }
}
