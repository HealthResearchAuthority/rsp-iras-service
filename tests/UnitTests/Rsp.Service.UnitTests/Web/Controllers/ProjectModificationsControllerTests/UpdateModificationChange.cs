using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class UpdateModificationChangeControllerTests : TestServiceBase<ProjectModificationsController>
{
    [Theory, AutoData]
    public async Task UpdateModificationChange_Sends_Command(UpdateModificationChangeRequest request)
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<UpdateModificationChangeCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await Sut.UpdateModificationChange(request);

        // Assert
        mediator.Verify(m => m.Send(It.Is<UpdateModificationChangeCommand>(c => c.ModificationChangeRequest == request), It.IsAny<CancellationToken>()), Times.Once);
    }
}