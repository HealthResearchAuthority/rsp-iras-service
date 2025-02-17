using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Services
{
    public interface IEmailNotificationService
    {
        Task<bool> SendEmail(SendEmailRequest requestData);
    }
}
