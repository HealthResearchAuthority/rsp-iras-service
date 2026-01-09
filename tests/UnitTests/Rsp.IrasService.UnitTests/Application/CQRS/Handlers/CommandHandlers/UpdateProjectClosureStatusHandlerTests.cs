using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class UpdateProjectClosureStatusHandlerTests
{
    [Fact]
    public async Task Handle_Delegates_To_Service()
    {
        // Arrange
        var serviceMock = new Mock<IProjectClosureService>();
        var handler = new UpdateProjectClosureStatusHandler(serviceMock.Object);

        const string projectRecordId = "PR1";
        const string status = "Authorised";
        const string userId = "user-123";

        var cmd = new UpdateProjectClosureStatusCommand
        {
            ProjectRecordId = projectRecordId,
            Status = status,
            UserId = userId
        };

        serviceMock
            .Setup(s => s.UpdateProjectClosureStatus(projectRecordId, status, userId))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await handler.Handle(cmd, CancellationToken.None);

        // Assert
        serviceMock.Verify(
            s => s.UpdateProjectClosureStatus(projectRecordId, status, userId),
            Times.Once);
    }
}