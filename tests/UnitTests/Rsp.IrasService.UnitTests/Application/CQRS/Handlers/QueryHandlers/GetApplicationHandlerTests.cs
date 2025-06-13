using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

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
            ProjectApplicationId = applicationId,
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
        result.ShouldNotBeNull();
        result.ProjectApplicationId.ShouldBe(expectedResponse.ProjectApplicationId);
        result.Title.ShouldBe(expectedResponse.Title);
        result.Description.ShouldBe(expectedResponse.Description);
        result.Status.ShouldBe(expectedResponse.Status);

        _applicationsServiceMock.Verify(service => service.GetApplication(applicationId), Times.Once);
    }
}