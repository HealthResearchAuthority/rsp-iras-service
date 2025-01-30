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
            ApplicationId = "App-123",
            Title = "Updated Project",
            Description = "Updated project description",
            UpdatedBy = "User-456",
            Status = "Approved"
        };

        var expectedResponse = new ApplicationResponse
        {
            ApplicationId = "App-123",
            Status = "Updated Successfully"
        };

        var command = new UpdateApplicationCommand(request);

        _applicationsServiceMock
            .Setup(service => service.UpdateApplication(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.ApplicationId, result.ApplicationId);
        Assert.Equal(expectedResponse.Status, result.Status);

        _applicationsServiceMock.Verify(service => service.UpdateApplication(request), Times.Once);
    }
}