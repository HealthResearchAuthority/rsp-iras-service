using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.UserNotificationsControllerTests;

public class GetUnreadUserNotificationsCountTests : TestServiceBase<UserNotificationsController>
{
    [Fact]
    public async Task GetUnreadUserNotificationsCount_ReturnsCountOfUnreadNotifications()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        const int expectedCount = 5;

        Mocker.GetMock<IMediator>()
            .Setup(m => m.Send(It.Is<GetUnreadUserNotificationsCountQuery>(q => q.UserId == userId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCount);

        // Act
        var result = await Sut.GetUnreadUserNotificationsCount(userId);

        // Assert
        result.ShouldBe(expectedCount);
    }
}