using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationChangeAnswersHandlerTests
{
    [Fact]
    public async Task Handle_NoCategory_Delegates_To_Service()
    {
        // Arrange
        var respondentSvc = new Mock<IRespondentService>();
        var handler = new GetModificationChangeAnswersHandler(respondentSvc.Object);
        var query = new GetModificationChangeAnswersQuery
        {
            ProjectModificationChangeId = Guid.NewGuid(),
            ProjectRecordId = "PR-1"
        };

        // Act
        await handler.Handle(query, CancellationToken.None);

        // Assert
        respondentSvc.Verify(s => s.GetModificationChangeResponses(query.ProjectModificationChangeId, query.ProjectRecordId), Times.Once);
    }

    [Fact]
    public async Task Handle_WithCategory_Delegates_To_Service()
    {
        // Arrange
        var respondentSvc = new Mock<IRespondentService>();
        var handler = new GetModificationChangeAnswersHandler(respondentSvc.Object);
        var query = new GetModificationChangeAnswersQuery
        {
            ProjectModificationChangeId = Guid.NewGuid(),
            ProjectRecordId = "PR-1",
            CategoryId = "C-1"
        };

        // Act
        await handler.Handle(query, CancellationToken.None);

        // Assert
        respondentSvc.Verify(s => s.GetModificationChangeResponses(query.ProjectModificationChangeId, query.ProjectRecordId, query.CategoryId!), Times.Once);
    }
}
