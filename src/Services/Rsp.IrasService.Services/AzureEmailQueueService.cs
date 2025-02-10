using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Domain.Entities.Notifications;
using Rsp.Logging.Extensions;

namespace Rsp.IrasService.Services
{
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
        public async Task SendMessageAsync<T>(T message) where T : INotificationMessage
        {
            await using var client = new ServiceBusClient(_connectionString);
            var sender = client.CreateSender(_queueName);

            try
            {
                string messageBody = JsonSerializer.Serialize(message);
                var serviceBusMessage = new ServiceBusMessage(messageBody);
                await sender.SendMessageAsync(serviceBusMessage);

                _logger.LogAsInformation($"Message sent to queue: {_queueName}");
            }
            catch (Exception ex)
            {
                _logger.LogAsError("SERVER_ERROR", "Error sending message to Azure Service Bus.", ex);
                throw;
            }
        }
    }
}
