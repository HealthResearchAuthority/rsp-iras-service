using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.Contracts.Services;

public interface ITriggerEmailNotificationService
{
    Task<bool> TriggerSendEmail(TriggerSendEmailRequest requestData);
}