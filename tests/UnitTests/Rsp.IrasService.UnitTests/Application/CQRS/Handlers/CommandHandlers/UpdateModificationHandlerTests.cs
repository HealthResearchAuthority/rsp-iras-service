using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class UpdateModificationHandlerTests : TestServiceBase<UpdateModificationHandler>
{
    [Theory, AutoData]
    public async Task Handle_Delegates_To_Service(UpdateModificationRequest request)
    {
        // Arrange
        var service = Mocker.GetMock<IProjectModificationService>();

        var command = new UpdateModificationCommand { ModificationRequest = request };

        // Act
        await Sut.Handle(command, CancellationToken.None);

        // Assert
        service.Verify(s => s.UpdateModification(request), Times.Once);
    }
}