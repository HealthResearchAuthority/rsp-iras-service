using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.CQRS.Handlers.CommandHandlers;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.CommandHandlers;

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