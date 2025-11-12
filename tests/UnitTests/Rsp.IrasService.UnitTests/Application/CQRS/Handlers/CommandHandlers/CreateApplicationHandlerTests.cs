using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class CreateApplicationHandlerTests
{
    private readonly Mock<IApplicationsService> _applicationsServiceMock;
    private readonly CreateApplicationHandler _handler;

    public CreateApplicationHandlerTests()
    {
        _applicationsServiceMock = new Mock<IApplicationsService>();
        _handler = new CreateApplicationHandler(_applicationsServiceMock.Object);
    }

    [Fact]
    public async Task Handle_CreateApplicationCommand_ShouldReturnExpectedResponse()
    {
        // Arrange
        var request = new ApplicationRequest
        {
            Id = "App-123",
            ShortProjectTitle = "New Project",
            FullProjectTitle = "A sample project",
            CreatedBy = "User-123",
            Status = "Pending"
        };

        var expectedResponse = new ApplicationResponse
        {
            Id = "App-123",
            Status = "Created Successfully"
        };

        var command = new CreateApplicationCommand(request);

        _applicationsServiceMock
            .Setup(service => service.CreateApplication(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(expectedResponse.Id);
        result.Status.ShouldBe(expectedResponse.Status);

        _applicationsServiceMock.Verify(service => service.CreateApplication(request), Times.Once);
    }
}