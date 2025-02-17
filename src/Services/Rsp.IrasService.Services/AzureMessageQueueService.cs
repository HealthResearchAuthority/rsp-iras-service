using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Contracts;
using Rsp.Logging.Extensions;

namespace Rsp.IrasService.Services;

public class AzureMessageQueueService : IMessageQueueService
{
    private readonly string _queueName;
    private readonly ILogger<AzureMessageQueueService> _logger;
    private readonly ServiceBusClient _client;

    public AzureMessageQueueService(IConfiguration configuration, ILogger<AzureMessageQueueService> logger, ServiceBusClient client)
    {
        _queueName = configuration["AppSettings:AzureServiceBus:QueueName"];
        _logger = logger;
        _client = client;
    }

    public async Task SendMessageToQueueAsync<T>(IEnumerable<T> messages)
    {
        await using (var sender = _client.CreateSender(_queueName))
        {
            var inputMessages = new List<ServiceBusMessage>();
            foreach (var message in messages)
            {
                string messageBody = JsonSerializer.Serialize(message);
                var serviceBusMessage = new ServiceBusMessage(messageBody);

                inputMessages.Add(serviceBusMessage);
            }

            await sender.SendMessagesAsync(inputMessages);
        }             

        _logger.LogAsInformation($"Message/s sent to queue: {_queueName}");
    }
}

