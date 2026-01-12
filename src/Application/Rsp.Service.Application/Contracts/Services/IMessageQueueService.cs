namespace Rsp.Service.Application.Contracts.Services;

public interface IMessageQueueService
{
    Task SendMessageToQueueAsync<T>(IEnumerable<T> messages);
}