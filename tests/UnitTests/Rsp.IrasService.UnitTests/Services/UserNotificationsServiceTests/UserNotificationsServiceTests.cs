using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.UserNotificationsServiceTests;

public class UserNotificationsServiceTests
{
    private readonly Mock<IUserNotificationsRepository> _repositoryMock;
    private readonly UserNotificationsService _service;

    public UserNotificationsServiceTests()
    {
        _repositoryMock = new Mock<IUserNotificationsRepository>();

        _service = new UserNotificationsService(
            _repositoryMock.Object
        );
    }

    [Fact]
    public async Task CreateUserNotification_ShouldAdaptRequestToEntity_AndCallRepository()
    {
        // Arrange
        var request = new CreateUserNotificationRequest
        {
            UserId = Guid.NewGuid().ToString(),
            Text = "Test notification",
            Type = "Info"
        };

        _repositoryMock
            .Setup(x => x.CreateUserNotification(It.IsAny<UserNotification>()))
            .ReturnsAsync((UserNotification n) => n);

        // Act
        await _service.CreateUserNotification(request);

        // Assert
        _repositoryMock.Verify(
            x => x.CreateUserNotification(It.Is<UserNotification>(n =>
                n.UserId == Guid.Parse(request.UserId) &&
                n.Text == request.Text)),
            Times.Once);
    }

    [Fact]
    public async Task GetUserNotifications_ShouldCallRepository_AndAdaptEntitiesToResponses()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var notifications = new List<UserNotification>
        {
            new() { UserId = Guid.Parse(userId), Text = "Notification 1" },
            new() { UserId = Guid.Parse(userId), Text = "Notification 2" }
        };

        _repositoryMock
            .Setup(x => x.GetUserNotifications(userId))
            .ReturnsAsync(notifications);

        // Act
        var result = await _service.GetUserNotifications(userId);

        // Assert
        _repositoryMock.Verify(x => x.GetUserNotifications(userId), Times.Once);

        result.Count().ShouldBe(2);
        result.First().UserId.ShouldBe(userId);
        result.First().Text.ShouldBe("Notification 1");
        result.First().ShouldBeOfType<UserNotificationResponse>();
    }

    [Fact]
    public async Task ReadNotifications_ShouldCallRepository()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        _repositoryMock
            .Setup(x => x.ReadNotifications(userId))
            .Returns(Task.CompletedTask);

        // Act
        await _service.ReadNotifications(userId);

        // Assert
        _repositoryMock.Verify(x => x.ReadNotifications(userId), Times.Once);
    }

    [Fact]
    public async Task GetUnreadUserNotificationsCount_ShouldCallRepository_AndReturnCount()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        _repositoryMock
            .Setup(x => x.GetUnreadUserNotificationsCount(userId))
            .ReturnsAsync(5);

        // Act
        var result = await _service.GetUnreadUserNotificationsCount(userId);

        // Assert
        _repositoryMock.Verify(x => x.GetUnreadUserNotificationsCount(userId), Times.Once);
        result.ShouldBe(5);
    }

    [Fact]
    public async Task AutoClearReadNotifications_ShouldCallRepository()
    {
        // Arrange
        _repositoryMock
            .Setup(x => x.AutoClearReadNotifications(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.AutoClearReadNotifications(20);

        // Assert
        _repositoryMock.Verify(
            x => x.AutoClearReadNotifications(20),
            Times.Once);
    }

    [Fact]
    public async Task AutoClearReadNotifications_ShouldOnlyCallRepositoryOnce()
    {
        // Arrange
        _repositoryMock
            .Setup(x => x.AutoClearReadNotifications(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.AutoClearReadNotifications(20);

        // Assert
        _repositoryMock.Verify(
            x => x.AutoClearReadNotifications(20),
            Times.Once);

        _repositoryMock.VerifyNoOtherCalls();
    }
}