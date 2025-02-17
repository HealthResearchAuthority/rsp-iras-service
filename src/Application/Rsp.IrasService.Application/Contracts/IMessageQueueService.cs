
namespace Rsp.IrasService.Application.Contracts;
public interface IMessageQueueService
{
    Task SendMessageToQueueAsync<T>(IEnumerable<T> messages);
}
