using Microsoft.AspNetCore.Mvc;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.UnitTests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.UserNotificationsControllerTests;

public class AutoClearReadNotificationsTests : TestServiceBase<UserNotificationsController>
{
    [Fact]
    public async Task AutoClearReadNotifications_ShouldSendCommand_AndReturnOk()
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<GetAutoClearUserNotificationsCommand>(c => c != null),
                default));

        // Act
        var result = await Sut.AutoClearReadNotifications(20);

        // Assert
        mockMediator.Verify(
            m => m.Send(
                It.Is<GetAutoClearUserNotificationsCommand>(c => c != null),
                default),
            Times.Once);

        result.ShouldBeOfType<OkResult>();
    }
}
