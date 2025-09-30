using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class UpdateModificationStatusHandlerTests
{
    [Fact]
    public async Task Handle_Delegates_To_Service()
    {
        // Arrange
        var service = new Mock<IProjectModificationService>();
        var handler = new UpdateModificationStatusHandler(service.Object);
        var id = Guid.NewGuid();
        const string status = "Submitted";
        var cmd = new UpdateModificationStatusCommand(id, status);

        // Act
        await handler.Handle(cmd, CancellationToken.None);

        // Assert
        service.Verify(s => s.UpdateModificationStatus(id, status), Times.Once);
    }
}