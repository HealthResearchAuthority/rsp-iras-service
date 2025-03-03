namespace Rsp.IrasService.Application.Contracts.Services;

public interface IMessageQueueService
{
    Task SendMessageToQueueAsync<T>(IEnumerable<T> messages);
}