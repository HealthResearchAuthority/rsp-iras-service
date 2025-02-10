using Rsp.IrasService.Domain.Entities.Notifications;

namespace Rsp.IrasService.Application.Contracts
{
    public interface IEmailMessageQueueService
    {
        Task SendMessageAsync<T>(T message) where T : INotificationMessage;
    }
}
