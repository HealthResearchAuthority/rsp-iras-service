using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class ModificationControllerMiscTests : TestServiceBase<ProjectModificationsController>
{
    [Fact]
    public async Task GetModificationChange_Sends_Query()
    {
        // Arrange
        var id = Guid.NewGuid();
        var mediator = Mocker.GetMock<IMediator>();
        mediator.Setup(x => x.Send(It.IsAny<GetModificationChangeQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ModificationChangeResponse());

        // Act
        var res = await Sut.GetModificationChange(id);

        // Assert
        res.ShouldNotBeNull();
        mediator.Verify(m => m.Send(It.Is<GetModificationChangeQuery>(q => q.ModificationChangeId == id), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetModificationChanges_Sends_Query()
    {
        // Arrange
        var id = Guid.NewGuid();
        var mediator = Mocker.GetMock<IMediator>();
        mediator.Setup(x => x.Send(It.IsAny<GetModificationChangesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ModificationChangeResponse>());

        // Act
        var res = await Sut.GetModificationChanges(id);

        // Assert
        res.ShouldNotBeNull();
        mediator.Verify(m => m.Send(It.Is<GetModificationChangesQuery>(q => q.ProjectModificationId == id), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task RemoveModificationChange_Sends_Command()
    {
        // Arrange
        var id = Guid.NewGuid();
        var mediator = Mocker.GetMock<IMediator>();
        mediator.Setup(x => x.Send(It.IsAny<RemoveModificationChangeCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await Sut.RemoveModificationChange(id);

        // Assert
        mediator.Verify(m => m.Send(It.Is<RemoveModificationChangeCommand>(q => q.ModificationChangeId == id), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateModificationStatus_Sends_Command()
    {
        // Arrange
        var id = Guid.NewGuid();
        const string status = "Submitted";
        var mediator = Mocker.GetMock<IMediator>();
        mediator.Setup(x => x.Send(It.IsAny<UpdateModificationStatusCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await Sut.UpdateModificationStatus(id, status);

        // Assert
        mediator.Verify(m => m.Send(It.Is<UpdateModificationStatusCommand>(q => q.ProjectModificationId == id && q.Status == status), It.IsAny<CancellationToken>()), Times.Once);
    }
}
