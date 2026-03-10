using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.UserNotificationsControllerTests;

public class CreateUserNotificationTests : TestServiceBase<UserNotificationsController>
{
    [Fact]
    public async Task CreateUserNotification_ReturnsCreatedNotification()
    {
        // Arrange
        var request = new CreateUserNotificationRequest
        {
            UserId = Guid.NewGuid().ToString(),
            Text = "Test notification",
            Type = "TestType"
        };

        var expectedResponse = new UserNotificationResponse
        {
            Id = Guid.NewGuid().ToString(),
            UserId = request.UserId,
            Text = request.Text,
            Type = request.Type,
            DateTimeCreated = DateTime.UtcNow
        };

        Mocker.GetMock<IMediator>()
            .Setup(m => m.Send(It.IsAny<CreateUserNotificationCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await Sut.CreateUserNotification(request);

        // Assert
        result.ShouldBeEquivalentTo(expectedResponse);
    }
}