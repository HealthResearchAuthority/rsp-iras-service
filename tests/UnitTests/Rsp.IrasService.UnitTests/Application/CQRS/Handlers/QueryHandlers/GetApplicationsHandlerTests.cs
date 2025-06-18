using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationsHandlerTests
{
    private readonly Mock<IApplicationsService> _applicationsServiceMock;
    private readonly GetApplicationsHandler _handler;

    public GetApplicationsHandlerTests()
    {
        _applicationsServiceMock = new Mock<IApplicationsService>();
        _handler = new GetApplicationsHandler(_applicationsServiceMock.Object);
    }

    [Fact]
    public async Task Handle_GetApplicationsQuery_ShouldReturnListOfApplications()
    {
        // Arrange
        var expectedResponse = new List<ApplicationResponse>
        {
            new() { Id = "App-123", Status = "Approved" },
            new() { Id = "App-456", Status = "Pending" }
        };

        var query = new GetApplicationsQuery();

        _applicationsServiceMock
            .Setup(service => service.GetApplications())
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Count().ShouldBe(expectedResponse.Count);

        _applicationsServiceMock.Verify(service => service.GetApplications(), Times.Once);
    }
}