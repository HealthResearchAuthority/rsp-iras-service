using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.Settings;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.AzureMessageQueueServiceTests;

public class AzureMessageQueueServiceTests
{
    private readonly Mock<ServiceBusClient> _mockServiceBusClient;
    private readonly Mock<ServiceBusSender> _mockServiceBusSender;
    private readonly Mock<ILogger<AzureMessageQueueService>> _mockLogger;
    private readonly AzureMessageQueueService _service;

    public AzureMessageQueueServiceTests()
    {
        _mockServiceBusClient = new Mock<ServiceBusClient>();
        _mockServiceBusSender = new Mock<ServiceBusSender>();
        _mockLogger = new Mock<ILogger<AzureMessageQueueService>>();

        // Setup mock client to return a sender
        _mockServiceBusClient
            .Setup(client => client.CreateSender(It.IsAny<string>()))
            .Returns(_mockServiceBusSender.Object);

        var appSettings = new AppSettings
        {
            AzureServiceBusSettings = new AzureServiceBusSettings
            {
                QueueName = "test-queue"
            }
        };

        _service = new AzureMessageQueueService(_mockLogger.Object, _mockServiceBusClient.Object, appSettings);
    }

    [Fact]
    public async Task SendsSingleMessage()
    {
        // Arrange
        var message = new EmailNotificationQueueMessage
        {
            EventName = "1",
            EmailTemplateId = "123"
        };

        // Act
        await _service.SendMessageToQueueAsync(new List<object> { message });

        // Assert

        var numberOfInvocationsForSendMessagesAsync = _mockServiceBusSender.Invocations.Count(x => x.Method.Name == nameof(ServiceBusSender.SendMessagesAsync));

        // check that SendMessagesAsync() is called once
        numberOfInvocationsForSendMessagesAsync.ShouldBe(1);
    }

    [Fact]
    public async Task SendsMultipleMessages()
    {
        // Arrange
        var messages = new List<EmailNotificationQueueMessage>
        {
            new EmailNotificationQueueMessage{ EventName = "1", EmailTemplateId = "112"},
            new EmailNotificationQueueMessage{ EventName = "2", EmailTemplateId = "122"},
            new EmailNotificationQueueMessage{ EventName = "3", EmailTemplateId = "172"}
        };

        // Act
        await _service.SendMessageToQueueAsync(messages);

        // Assert

        var numberOfInvocationsForSendMessagesAsync = _mockServiceBusSender.Invocations.Count(x => x.Method.Name == nameof(ServiceBusSender.SendMessagesAsync));

        // check that SendMessagesAsync() is called once
        numberOfInvocationsForSendMessagesAsync.ShouldBe(1);
    }

    [Fact]
    public async Task EmptyList_DoesNotSend()
    {
        // Arrange
        var messages = new List<EmailNotificationQueueMessage>();

        // Act
        await _service.SendMessageToQueueAsync(messages);

        // Assert

        var numberOfInvocationsForSendMessagesAsync = _mockServiceBusSender.Invocations.Count(x => x.Method.Name == nameof(ServiceBusSender.SendMessagesAsync));

        // check that SendMessagesAsync() is not called
        numberOfInvocationsForSendMessagesAsync.ShouldBe(0);
    }
}