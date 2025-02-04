﻿namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationWithStatusHandlerTests
{
    private readonly Mock<IApplicationsService> _applicationsServiceMock;
    private readonly GetApplicationWithStatusHandler _handler;

    public GetApplicationWithStatusHandlerTests()
    {
        _applicationsServiceMock = new Mock<IApplicationsService>();
        _handler = new GetApplicationWithStatusHandler(_applicationsServiceMock.Object);
    }

    [Fact]
    public async Task Handle_GetApplicationWithStatusQuery_ShouldReturnApplication()
    {
        // Arrange
        var applicationId = "App-123";
        var status = "Approved";
        var expectedResponse = new ApplicationResponse
        {
            ApplicationId = applicationId,
            Status = status
        };

        // ✅ Ensure query is correctly initialized and `ApplicationStatus` is explicitly set.
        var query = new GetApplicationWithStatusQuery(applicationId)
        {
            ApplicationStatus = status
        };

        _applicationsServiceMock
            .Setup(service => service.GetApplication(applicationId, status))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.ApplicationId, result.ApplicationId);
        Assert.Equal(expectedResponse.Status, result.Status);

        _applicationsServiceMock.Verify(service => service.GetApplication(applicationId, status), Times.Once);
    }
}