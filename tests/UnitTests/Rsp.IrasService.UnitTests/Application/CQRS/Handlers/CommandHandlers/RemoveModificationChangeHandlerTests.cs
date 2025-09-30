using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class RemoveModificationChangeHandlerTests
{
    [Fact]
    public async Task Handle_Delegates_To_Service()
    {
        // Arrange
        var service = new Mock<IProjectModificationService>();
        var handler = new RemoveModificationChangeHandler(service.Object);
        var id = Guid.NewGuid();
        var cmd = new RemoveModificationChangeCommand(id);

        // Act
        await handler.Handle(cmd, CancellationToken.None);

        // Assert
        service.Verify(s => s.RemoveModificationChange(id), Times.Once);
    }
}
