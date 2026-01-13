using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.CQRS.Handlers.CommandHandlers;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class UpdateModificationChangeHandlerTests : TestServiceBase<UpdateModificationChangeHandler>
{
    [Theory, AutoData]
    public async Task Handle_Delegates_To_Service(UpdateModificationChangeRequest request)
    {
        // Arrange
        var service = Mocker.GetMock<IProjectModificationService>();

        var command = new UpdateModificationChangeCommand
        {
            ModificationChangeRequest = request
        };

        // Act
        await Sut.Handle(command, CancellationToken.None);

        // Assert
        service.Verify(s => s.UpdateModificationChange(request), Times.Once);
    }
}