using Rsp.Service.Application.Contracts.Repositories;
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