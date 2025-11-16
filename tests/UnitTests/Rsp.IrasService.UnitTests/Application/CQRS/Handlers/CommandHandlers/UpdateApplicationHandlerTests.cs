using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class UpdateApplicationHandlerTests : TestServiceBase<UpdateApplicationHandler>
{
    [Theory, AutoData]
    public async Task Handle_UpdateApplicationCommand_ShouldReturnExpectedResponse(ApplicationRequest request)
    {
        // Arrange
        var expectedResponse = new ApplicationResponse
        {
            Id = "App-123",
            Status = "Updated Successfully"
        };

        var command = new UpdateApplicationCommand(request);

        var applicationService = Mocker.GetMock<IApplicationsService>();

        applicationService
            .Setup(service => service.UpdateApplication(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await Sut.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(expectedResponse.Id);
        result.Status.ShouldBe(expectedResponse.Status);

        applicationService.Verify(service => service.UpdateApplication(request), Times.Once);
    }
}