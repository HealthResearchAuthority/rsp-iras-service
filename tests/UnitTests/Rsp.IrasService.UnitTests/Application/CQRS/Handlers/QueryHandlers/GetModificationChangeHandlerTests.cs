using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Handlers.QueryHandlers;
using Rsp.Service.Application.CQRS.Queries;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationChangeHandlerTests
{
    [Fact]
    public async Task Handle_Delegates_To_Service()
    {
        // Arrange
        var svc = new Mock<IProjectModificationService>();
        var handler = new GetModificationChangeHandler(svc.Object);
        var id = Guid.NewGuid();
        var query = new GetModificationChangeQuery
        {
            ModificationChangeId = id
        };

        // Act
        await handler.Handle(query, CancellationToken.None);

        // Assert
        svc.Verify(s => s.GetModificationChange(id), Times.Once);
    }
}