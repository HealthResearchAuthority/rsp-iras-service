using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Commands;

public class UpdateModificationStatusCommandTests
{
    private readonly Mock<IMediator> _mediatorMock = new();

    [Fact]
    public void Ctor_Sets_ModificationId_And_Status()
    {
        // Arrange
        var id = Guid.NewGuid();
        const string status = "Approved";

        // Act
        var cmd = new UpdateModificationStatusCommand(id, status);

        // Assert
        cmd.ProjectModificationId.ShouldBe(id);
        cmd.Status.ShouldBe(status);
    }

    [Fact]
    public async Task Send_UpdateModificationStatusCommand_ShouldInvokeMediator()
    {
        // Arrange
        var id = Guid.NewGuid();
        const string status = "Submitted";
        var command = new UpdateModificationStatusCommand(id, status);

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _mediatorMock.Object.Send(command, CancellationToken.None);

        // Assert
        _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
    }
}