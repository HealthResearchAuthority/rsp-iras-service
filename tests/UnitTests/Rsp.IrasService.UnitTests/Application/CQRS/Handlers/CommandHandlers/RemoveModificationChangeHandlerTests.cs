using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.CommandHandlers;

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
