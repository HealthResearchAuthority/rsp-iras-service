using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.Settings;
using Rsp.Logging.Extensions;

namespace Rsp.IrasService.Services;

public class AzureMessageQueueService : IMessageQueueService
{
    private readonly string? _queueName;
    private readonly ILogger<AzureMessageQueueService> _logger;
    private readonly ServiceBusClient _client;

    public AzureMessageQueueService(ILogger<AzureMessageQueueService> logger,
        ServiceBusClient client,
        AppSettings appSettings)
    {
        _queueName = appSettings?.AzureServiceBusSettings?.QueueName;
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
                var messageBody = JsonSerializer.Serialize(message);
                var serviceBusMessage = new ServiceBusMessage(messageBody);

                inputMessages.Add(serviceBusMessage);
            }

            await sender.SendMessagesAsync(inputMessages);
        }

        var messageParameters = $"QueueName: {_queueName}";
        _logger.LogAsInformation(parameters: messageParameters, "Message/s sent to queue.");
    }
}