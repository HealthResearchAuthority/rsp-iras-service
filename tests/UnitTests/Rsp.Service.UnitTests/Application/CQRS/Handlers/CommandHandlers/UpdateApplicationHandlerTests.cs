using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.CQRS.Handlers.CommandHandlers;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.CommandHandlers;

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