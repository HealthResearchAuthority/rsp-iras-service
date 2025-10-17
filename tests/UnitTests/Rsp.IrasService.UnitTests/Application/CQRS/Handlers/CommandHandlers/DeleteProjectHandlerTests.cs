using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class DeleteProjectHandlerTests : TestServiceBase<DeleteProjectHandler>
{
    [Theory, AutoData]
    public async Task Calls_Service_DeleteProject_With_Correct_Id(string projectRecordId)
    {
        // Arrange
        var service = new Mock<IApplicationsService>();

        service
            .Setup(s => s.DeleteProject(projectRecordId))
            .Returns(Task.CompletedTask).Verifiable();

        Mocker.Use(service.Object);

        Sut = Mocker.CreateInstance<DeleteProjectHandler>();

        var command = new DeleteProjectCommand(projectRecordId);

        // Act
        await Sut.Handle(command, CancellationToken.None);

        // Assert
        service.Verify(s => s.DeleteProject(projectRecordId), Times.Once);
    }
}