namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationsWithRespondentHandlerTests
{
    private readonly Mock<IApplicationsService> _applicationsServiceMock;
    private readonly GetApplicationsWithRespondentHandler _handler;

    public GetApplicationsWithRespondentHandlerTests()
    {
        _applicationsServiceMock = new Mock<IApplicationsService>();
        _handler = new GetApplicationsWithRespondentHandler(_applicationsServiceMock.Object);
    }

    [Fact]
    public async Task Handle_GetApplicationsWithRespondentQuery_ShouldReturnListOfApplications()
    {
        // Arrange
        var respondentId = "R-123";
        var expectedResponse = new List<ApplicationResponse>
        {
            new() { ApplicationId = "App-123", Status = "Approved" }
        };

        var query = new GetApplicationsWithRespondentQuery { RespondentId = respondentId };

        _applicationsServiceMock
            .Setup(service => service.GetRespondentApplications(respondentId))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        _applicationsServiceMock.Verify(service => service.GetRespondentApplications(respondentId), Times.Once);
    }
}