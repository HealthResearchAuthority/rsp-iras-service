using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationAuditTrailHandlerTests
{
    [Fact]
    public async Task Handle_Delegates_To_Service()
    {
        // Arrange
        var svc = new Mock<IProjectModificationService>();
        var handler = new GetModificationAuditTrailHandler(svc.Object);
        var id = Guid.NewGuid();
        var query = new GetModificationAuditTrailQuery
        {
            ProjectModificationId = id
        };

        // Act
        await handler.Handle(query, CancellationToken.None);

        // Assert
        svc.Verify(s => s.GetModificationAuditTrail(id), Times.Once);
    }
}