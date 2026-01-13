using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class UpdateModificationControllerTests : TestServiceBase<ProjectModificationsController>
{
    [Theory, AutoData]
    public async Task UpdateModification_Sends_Command(UpdateModificationRequest request)
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<UpdateModificationCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await Sut.UpdateModification(request);

        // Assert
        mediator.Verify(m => m.Send(It.Is<UpdateModificationCommand>(c => c.ModificationRequest == request), It.IsAny<CancellationToken>()), Times.Once);
    }
}