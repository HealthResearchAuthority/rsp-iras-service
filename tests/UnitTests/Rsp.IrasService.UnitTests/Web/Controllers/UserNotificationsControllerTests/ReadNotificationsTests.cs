using Microsoft.AspNetCore.Mvc;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.UserNotificationsControllerTests;

public class ReadNotificationsTests : TestServiceBase<UserNotificationsController>
{
    [Fact]
    public async Task ReadNotifications_MarksNotificationsAsRead()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();

        Mocker.GetMock<IMediator>()
            .Setup(m => m.Send(It.IsAny<ReadNotificationsCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await Sut.ReadNotifications(userId);

        // Assert
        result.ShouldBeOfType<OkResult>();

        Mocker.GetMock<IMediator>()
            .Verify(m => m.Send(It.Is<ReadNotificationsCommand>(q => q.UserId == userId), It.IsAny<CancellationToken>()), Times.Once);
    }
}