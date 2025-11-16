using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

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