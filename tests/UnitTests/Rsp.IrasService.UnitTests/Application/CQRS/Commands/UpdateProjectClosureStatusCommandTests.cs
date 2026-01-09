using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Commands;

public class UpdateProjectClosureStatusCommandTests
{
    private readonly Mock<IMediator> _mediatorMock = new();

    [Fact]
    public async Task Send_UpdateProjectClosureStatusCommand_ShouldInvokeMediator()
    {
        // Arrange
        const string projectRecordId = "PR1";
        const string status = "Authorised";
        const string userId = "user-123";

        var command = new UpdateProjectClosureStatusCommand
        {
            ProjectRecordId = projectRecordId,
            Status = status,
            UserId = userId
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _mediatorMock.Object.Send(command, CancellationToken.None);

        // Assert
        _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
    }
}