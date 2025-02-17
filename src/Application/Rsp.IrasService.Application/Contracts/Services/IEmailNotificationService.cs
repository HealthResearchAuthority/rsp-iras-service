using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.Contracts.Services;
public interface IEmailNotificationService
{
    Task<bool> SendEmail(SendEmailRequest requestData);
}
