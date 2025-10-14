using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ApplicationsControllerTests;

public class DeleteProjectRecord
{
    [Fact]
    public async Task Calls_Mediator_With_DeleteProjectCommand()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        var controller = new ApplicationsController(mediator.Object);
        var projectRecordId = "PR-123";

        mediator
            .Setup(m => m.Send(It.Is<DeleteProjectCommand>(cmd => cmd.ProjectRecordId == projectRecordId), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await controller.DeleteProjectRecord(projectRecordId);

        // Assert
        mediator.Verify(m => m.Send(It.Is<DeleteProjectCommand>(cmd => cmd.ProjectRecordId == projectRecordId), It.IsAny<CancellationToken>()), Times.Once);
    }
}