using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.TriggerEmailNotificationServiceTests;

public class TriggerEmailNotificationServiceTest
{
    private readonly Mock<IEmailTemplateService> _templateServiceMock;
    private readonly Mock<IMessageQueueService> _mqServiceMock;
    private readonly TriggerEmailNotificationService _service;

    public TriggerEmailNotificationServiceTest()
    {
        _templateServiceMock = new Mock<IEmailTemplateService>();
        _mqServiceMock = new Mock<IMessageQueueService>();

        _service = new TriggerEmailNotificationService(
            _templateServiceMock.Object,
            _mqServiceMock.Object
        );
    }

    [Fact]
    public async Task TriggerSendEmail_ShouldReturnTrue_AndSendMessages_WhenTemplateExists()
    {
        // Arrange
        var request = new TriggerSendEmailRequest
        {
            EventType = "event123",
            EmailRecipients = new List<string> { "test1@email.com", "test2@email.com" },
            Data = new Dictionary<string, dynamic> {
                { "FirstName", "John" },
                { "LastName", "Smith" }
            }
        };

        var template = new EmailTemplate
        {
            TemplateId = "template123",
            EventType = new EventType { Id = "event123", EventName = "Application_Created" }
        };

        _templateServiceMock
            .Setup(x => x.GetEmailTemplateForEventType(request.EventType))
            .ReturnsAsync(template);

        // Act
        var result = await _service.TriggerSendEmail(request);

        // Assert
        // SendMessageToQueue should be called once
        // there should be two messages sent because of 2 email addresses
        result.ShouldBeTrue();
        _mqServiceMock.Verify(x => x.SendMessageToQueueAsync(
            It.Is<List<EmailNotificationQueueMessage>>(m =>
                m.Count == 2 &&
                m[0].EmailTemplateId == "template123" &&
                m[0].RecipientAdress == "test1@email.com" &&
                m[1].RecipientAdress == "test2@email.com"
            )), Times.Once);
    }

    [Fact]
    public async Task ShouldReturnFalse_WhenTemplateDoesNotExist()
    {
        // Arrange
        var request = new TriggerSendEmailRequest
        {
            EventType = "event123",
            EmailRecipients = new List<string> { "test1@email.com" },
            Data = new Dictionary<string, dynamic> {
                { "FirstName", "John" },
                { "LastName", "Smith" }
            }
        };

        _templateServiceMock
            .Setup(x => x.GetEmailTemplateForEventType(request.EventType))
            .ReturnsAsync((EmailTemplate)null);

        // Act
        var result = await _service.TriggerSendEmail(request);

        // Assert
        // SendMessageToQueueAsync is never called due to template not existing
        result.ShouldBeFalse();
        _mqServiceMock.Verify(x => x.SendMessageToQueueAsync(It.IsAny<List<EmailNotificationQueueMessage>>()), Times.Never);
    }

    [Fact]
    public async Task ShouldSendCorrectNumberOfMessages()
    {
        // Arrange
        var request = new TriggerSendEmailRequest
        {
            EventType = "event123",
            EmailRecipients = new List<string>
            {
                "user1@email.com",
                "user2@email.com",
                "user3@email.com"
            },
            Data = new Dictionary<string, dynamic> {
                { "FirstName", "John" },
                { "LastName", "Smith" }
            }
        };

        var template = new EmailTemplate
        {
            TemplateId = "template123",
            EventType = new EventType { Id = "event123", EventName = "Application Created" }
        };

        _templateServiceMock
            .Setup(x => x.GetEmailTemplateForEventType(request.EventType))
            .ReturnsAsync(template);

        // Act
        var result = await _service.TriggerSendEmail(request);

        // Assert
        // sends three messages to the queue because of the three email addresses
        result.ShouldBeTrue();
        _mqServiceMock.Verify(x => x.SendMessageToQueueAsync(
            It.Is<List<EmailNotificationQueueMessage>>(m => m.Count == 3)), Times.Once);
    }
}