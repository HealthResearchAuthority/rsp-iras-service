using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationChangesHandlerTests
{
    [Fact]
    public async Task Handle_Delegates_To_Service()
    {
        // Arrange
        var svc = new Mock<IProjectModificationService>();
        var handler = new GetModificationChangesHandler(svc.Object);
        var id = Guid.NewGuid();
        var query = new GetModificationChangesQuery(id);

        // Act
        await handler.Handle(query, CancellationToken.None);

        // Assert
        svc.Verify(s => s.GetModificationChanges(id), Times.Once);
    }
}