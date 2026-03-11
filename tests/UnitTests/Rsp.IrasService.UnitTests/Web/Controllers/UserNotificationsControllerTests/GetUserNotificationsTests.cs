using Rsp.IrasService.Application.DTOS.Responses;
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
        var totalCount = 5;
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
        var expectedResult = new UserNotificationsResponse
        {
            Notifications = expectedNotifications,
            TotalCount = totalCount
        };

        Mocker.GetMock<IMediator>()
            .Setup(m => m.Send(It.Is<GetUserNotificationsQuery>(q => q.UserId == userId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await Sut.GetUserNotifications(userId, 1, 20, "date", "desc", null);

        // Assert
        result.Notifications.ShouldBeEquivalentTo(expectedNotifications);
        result.TotalCount.ShouldBe(totalCount);
    }
}