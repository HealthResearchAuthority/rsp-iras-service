using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.Contracts.Services;

public interface ITriggerEmailNotificationService
{
    Task<bool> TriggerSendEmail(TriggerSendEmailRequest requestData);
}