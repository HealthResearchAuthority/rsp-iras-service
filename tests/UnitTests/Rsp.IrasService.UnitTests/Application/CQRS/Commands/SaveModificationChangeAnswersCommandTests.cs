using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Commands;

public class SaveModificationChangeAnswersCommandTests
{
    private readonly Mock<IMediator> _mediatorMock = new();

    [Theory, AutoData]
    public void Ctor_Sets_Request(ModificationChangeAnswersRequest request)
    {
        // Act
        var cmd = new SaveModificationChangeAnswersCommand(request);

        // Assert
        cmd.ModificationAnswersRequest.ShouldBe(request);
    }

    [Theory, AutoData]
    public async Task Send_SaveModificationChangeAnswersCommand_ShouldInvokeMediator(ModificationChangeAnswersRequest request)
    {
        // Arrange
        var command = new SaveModificationChangeAnswersCommand(request);

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _mediatorMock.Object.Send(command, CancellationToken.None);

        // Assert
        _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
    }
}
