using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.UserNotificationsControllerTests;

public class GetUserNotificationsTests : TestServiceBase<UserNotificationsController>
{
    [Fact]
    public async Task GetUserNotifications_ReturnsNotificationsForUser()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var expectedNotifications = new List<UserNotificationResponse>
            {
                new() {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    Text = "Test notification 1",
                    Type = "TestType",
                    DateTimeCreated = DateTime.UtcNow
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    Text = "Test notification 2",
                    Type = "TestType",
                    DateTimeCreated = DateTime.UtcNow.AddMinutes(-5)
                }
            };

        Mocker.GetMock<IMediator>()
            .Setup(m => m.Send(It.Is<GetUserNotificationsQuery>(q => q.UserId == userId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedNotifications);

        // Act
        var result = await Sut.GetUserNotifications(userId);

        // Assert
        result.ShouldBeEquivalentTo(expectedNotifications);
    }
}