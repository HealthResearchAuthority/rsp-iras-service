using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class DeleteProjectHandlerTests : TestServiceBase<DeleteProjectHandler>
{
    [Theory, AutoData]
    public async Task Calls_Service_DeleteProject_With_Correct_Id(string projectRecordId)
    {
        // Arrange
        var service = Mocker.GetMock<IApplicationsService>();

        service
            .Setup(s => s.DeleteProject(projectRecordId))
            .Returns(Task.CompletedTask).Verifiable();

        var command = new DeleteProjectCommand(projectRecordId);

        // Act
        await Sut.Handle(command, CancellationToken.None);

        // Assert
        service.Verify(s => s.DeleteProject(projectRecordId), Times.Once);
    }
}