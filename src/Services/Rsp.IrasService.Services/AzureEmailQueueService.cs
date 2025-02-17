using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Contracts;
using Rsp.Logging.Extensions;

namespace Rsp.IrasService.Services;

public class AzureEmailQueueService : IEmailMessageQueueService
{
    private readonly string _connectionString;
    private readonly string _queueName;
    private readonly ILogger<AzureEmailQueueService> _logger;

    public AzureEmailQueueService(IConfiguration configuration, ILogger<AzureEmailQueueService> logger)
    {
        _connectionString = configuration["AppSettings:AzureServiceBus:ConnectionString"];
        _queueName = configuration["AppSettings:AzureServiceBus:QueueName"];
        _logger = logger;
    }

    public async Task SendMessageToQueueAsync<T>(IEnumerable<T> messages)
    {
        await using var client = new ServiceBusClient(_connectionString);
        var sender = client.CreateSender(_queueName);

        var inputMessages = new List<ServiceBusMessage>();
        foreach (var message in messages)
        {
            string messageBody = JsonSerializer.Serialize(message);
            var serviceBusMessage = new ServiceBusMessage(messageBody);

            inputMessages.Add(serviceBusMessage);
        }

        await sender.SendMessagesAsync(inputMessages);

        _logger.LogAsInformation($"Message/s sent to queue: {_queueName}");

    }
}

