using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class SendEmailCommandHandler(ITriggerEmailNotificationService service)
    : IRequestHandler<SendEmailCommand, SendEmailResponse>
{
    public async Task<SendEmailResponse> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var sendEmail = await service.TriggerSendEmail(request.SendEmailRequest);

        return new SendEmailResponse
        {
            IsSuccess = sendEmail
        };
    }
}