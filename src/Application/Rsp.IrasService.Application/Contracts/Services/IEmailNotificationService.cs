using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Services
{
    public interface IEmailNotificationService
    {
        /// <summary>
        /// Send an email
        /// </summary>
        /// <param name="eventTypeId">The id of the event type</param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Task<bool> SendEmail(SendEmailRequest requestData);
    }
}
