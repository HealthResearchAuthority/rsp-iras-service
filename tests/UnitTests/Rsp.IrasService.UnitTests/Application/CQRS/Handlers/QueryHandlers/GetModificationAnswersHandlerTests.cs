using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationAnswersHandlerTests
{
    [Fact]
    public async Task Handle_NoCategory_Delegates_To_Service()
    {
        // Arrange
        var respondentSvc = new Mock<IRespondentService>();
        var handler = new GetModificationAnswersHandler(respondentSvc.Object);
        var query = new GetModificationAnswersQuery
        {
            ProjectModificationId = Guid.NewGuid(),
            ProjectRecordId = "PR-1",
            CategoryId = null
        };

        // Act
        await handler.Handle(query, CancellationToken.None);

        // Assert
        respondentSvc.Verify(s => s.GetModificationResponses(query.ProjectModificationId, query.ProjectRecordId), Times.Once);
    }

    [Fact]
    public async Task Handle_WithCategory_Delegates_To_Service()
    {
        // Arrange
        var respondentSvc = new Mock<IRespondentService>();
        var handler = new GetModificationAnswersHandler(respondentSvc.Object);
        var query = new GetModificationAnswersQuery
        {
            ProjectModificationId = Guid.NewGuid(),
            ProjectRecordId = "PR-1",
            CategoryId = "Cat-1"
        };

        // Act
        await handler.Handle(query, CancellationToken.None);

        // Assert
        respondentSvc.Verify(s => s.GetModificationResponses(query.ProjectModificationId, query.ProjectRecordId, query.CategoryId!), Times.Once);
    }
}
