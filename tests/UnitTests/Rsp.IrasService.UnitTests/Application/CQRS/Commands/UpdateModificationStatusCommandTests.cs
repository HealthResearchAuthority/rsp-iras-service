using Rsp.Service.Application.CQRS.Commands;

namespace Rsp.Service.UnitTests.Application.CQRS.Commands;

public class UpdateModificationStatusCommandTests
{
    private readonly Mock<IMediator> _mediatorMock = new();

    [Fact]
    public async Task Send_UpdateModificationStatusCommand_ShouldInvokeMediator()
    {
        // Arrange
        var id = Guid.NewGuid();
        const string status = "Submitted";
        var command = new UpdateModificationStatusCommand
        {
            ProjectRecordId = "PR1",
            ProjectModificationId = id,
            Status = status,
            RevisionDescription = null
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