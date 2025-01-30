namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationHandlerTests
{
    private readonly Mock<IApplicationsService> _applicationsServiceMock;
    private readonly GetApplicationHandler _handler;

    public GetApplicationHandlerTests()
    {
        _applicationsServiceMock = new Mock<IApplicationsService>();
        _handler = new GetApplicationHandler(_applicationsServiceMock.Object);
    }

    [Fact]
    public async Task Handle_GetApplicationQuery_ShouldReturnExpectedResponse()
    {
        // Arrange
        var applicationId = "App-123";

        var expectedResponse = new ApplicationResponse
        {
            ApplicationId = applicationId,
            Title = "Sample Project",
            Description = "A sample project description",
            Status = "Approved"
        };

        var query = new GetApplicationQuery(applicationId);

        _applicationsServiceMock
            .Setup(service => service.GetApplication(applicationId))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.ApplicationId, result.ApplicationId);
        Assert.Equal(expectedResponse.Title, result.Title);
        Assert.Equal(expectedResponse.Description, result.Description);
        Assert.Equal(expectedResponse.Status, result.Status);

        _applicationsServiceMock.Verify(service => service.GetApplication(applicationId), Times.Once);
    }
}