using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Handlers.QueryHandlers;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationsWithStatusHandlerTests
{
    private readonly Mock<IApplicationsService> _applicationsServiceMock;
    private readonly GetApplicationsWithStatusHandler _handler;

    public GetApplicationsWithStatusHandlerTests()
    {
        _applicationsServiceMock = new Mock<IApplicationsService>();
        _handler = new GetApplicationsWithStatusHandler(_applicationsServiceMock.Object);
    }

    [Fact]
    public async Task Handle_GetApplicationsWithStatusQuery_ShouldReturnApplicationsByStatus()
    {
        // Arrange
        var status = "Approved";
        var expectedResponse = new List<ApplicationResponse>
        {
            new() { Id = "App-123", Status = status }
        };

        var query = new GetApplicationsWithStatusQuery { ApplicationStatus = status };

        _applicationsServiceMock
            .Setup(service => service.GetApplications(status))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldHaveSingleItem();
        _applicationsServiceMock.Verify(service => service.GetApplications(status), Times.Once);
    }
}