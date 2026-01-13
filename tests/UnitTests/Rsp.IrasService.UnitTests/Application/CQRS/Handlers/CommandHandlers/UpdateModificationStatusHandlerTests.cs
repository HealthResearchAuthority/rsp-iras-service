using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.CommandHandlers;

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
        var cmd = new UpdateModificationStatusCommand
        {
            ProjectRecordId = "PR1",
            ProjectModificationId = id,
            Status = status
        };

        // Act
        await handler.Handle(cmd, CancellationToken.None);

        // Assert
        service.Verify(s => s.UpdateModificationStatus("PR1", id, status), Times.Once);
    }
}