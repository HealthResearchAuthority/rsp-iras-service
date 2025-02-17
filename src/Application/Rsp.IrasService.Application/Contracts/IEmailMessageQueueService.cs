using Rsp.IrasService.Application.Contracts;

namespace Rsp.IrasService.Application.Contracts
{
    public interface IEmailMessageQueueService
    {
        Task SendMessageToQueueAsync<T>(IEnumerable<T> messages);
    }
}
