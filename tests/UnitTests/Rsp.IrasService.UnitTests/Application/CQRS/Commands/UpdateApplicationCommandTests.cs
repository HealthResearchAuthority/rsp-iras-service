using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Commands;

public class UpdateApplicationHandlerTests
{
    private readonly Mock<IApplicationsService> _applicationsServiceMock;
    private readonly UpdateApplicationHandler _handler;

    public UpdateApplicationHandlerTests()
    {
        _applicationsServiceMock = new Mock<IApplicationsService>();
        _handler = new UpdateApplicationHandler(_applicationsServiceMock.Object);
    }

    [Fact]
    public async Task Handle_UpdateApplicationCommand_ShouldReturnExpectedResponse()
    {
        // Arrange
        var request = new ApplicationRequest
        {
            ProjectApplicationId = "App-123",
            Title = "Updated Project",
            Description = "Updated project description",
            UpdatedBy = "User-456",
            Status = "Approved"
        };

        var expectedResponse = new ApplicationResponse
        {
            ProjectApplicationId = "App-123",
            Status = "Updated Successfully"
        };

        var command = new UpdateApplicationCommand(request);

        _applicationsServiceMock
            .Setup(service => service.UpdateApplication(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ProjectApplicationId.ShouldBe(expectedResponse.ProjectApplicationId);
        result.Status.ShouldBe(expectedResponse.Status);

        _applicationsServiceMock.Verify(service => service.UpdateApplication(request), Times.Once);
    }
}