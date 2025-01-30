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
            ApplicationId = "App-123",
            Title = "New Project",
            Description = "A sample project",
            CreatedBy = "User-123",
            Status = "Pending"
        };

        var expectedResponse = new ApplicationResponse
        {
            ApplicationId = "App-123",
            Status = "Created Successfully"
        };

        var command = new CreateApplicationCommand(request);

        _applicationsServiceMock
            .Setup(service => service.CreateApplication(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.ApplicationId, result.ApplicationId);
        Assert.Equal(expectedResponse.Status, result.Status);

        _applicationsServiceMock.Verify(service => service.CreateApplication(request), Times.Once);
    }
}