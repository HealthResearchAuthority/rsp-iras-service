using Rsp.Service.Application.CQRS.Commands;

namespace Rsp.Service.UnitTests.Application.CQRS.Commands;

public class RemoveModificationChangeCommandTests
{
    private readonly Mock<IMediator> _mediatorMock = new();

    [Fact]
    public void Ctor_Sets_ModificationChangeId()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var cmd = new RemoveModificationChangeCommand(id);

        // Assert
        cmd.ModificationChangeId.ShouldBe(id);
    }

    [Fact]
    public async Task Send_RemoveModificationChangeCommand_ShouldInvokeMediator()
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new RemoveModificationChangeCommand(id);

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _mediatorMock.Object.Send(command, CancellationToken.None);

        // Assert
        _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
    }
}
