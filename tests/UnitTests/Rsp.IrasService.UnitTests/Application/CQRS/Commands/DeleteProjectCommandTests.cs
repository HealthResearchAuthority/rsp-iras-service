using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Commands;

public class DeleteProjectCommandTests
{
    private readonly Mock<IMediator> _mediatorMock;

    public DeleteProjectCommandTests()
    {
        _mediatorMock = new Mock<IMediator>();
    }

    [Fact]
    public async Task Send_DeleteProjectCommand_ShouldCallMediatorWithCorrectId()
    {
        // Arrange
        var projectRecordId = "Test-Delete-123";
        var command = new DeleteProjectCommand(projectRecordId);

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _mediatorMock.Object.Send(command, CancellationToken.None);

        // Assert
        _mediatorMock.Invocations.Count.ShouldBe(1);
        _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory, AutoData]
    public void Sets_ProjectRecordId_Correctly(string projectRecordId)
    {
        // Act
        var command = new DeleteProjectCommand(projectRecordId);

        // Assert
        command.ProjectRecordId.ShouldBe(projectRecordId);
    }

    [Fact]
    public void Can_Set_ProjectRecordId_Property()
    {
        // Arrange
        var initialId = "initial-id";
        var newId = "new-id";
        var command = new DeleteProjectCommand(initialId)
        {
            // Act
            ProjectRecordId = newId
        };

        // Assert
        command.ProjectRecordId.ShouldBe(newId);
    }
}