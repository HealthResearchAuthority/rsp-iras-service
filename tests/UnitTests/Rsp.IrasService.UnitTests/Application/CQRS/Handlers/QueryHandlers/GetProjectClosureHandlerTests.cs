using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetProjectClosureHandlerTests
{
    private readonly Mock<IProjectClosureService> _projectClosureServiceMock;
    private readonly GetProjectClosureHandler _handler;

    public GetProjectClosureHandlerTests()
    {
        _projectClosureServiceMock = new Mock<IProjectClosureService>();
        _handler = new GetProjectClosureHandler(_projectClosureServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ProjectClosurQuery_ShouldReturnExpectedResponse()
    {
        // Arrange
        var projectRecordId = "123";
        var expectedClosures = new List<ProjectClosureResponse>
        {
            new()
            {
                Id = Guid.NewGuid(),
                ProjectRecordId = "PR-1001",
                ShortProjectTitle = "Alpha Study",
                Status = "With sponsor",
                IrasId = 123,
                ProjectClosureNumber=1,
                TransactionId="C12345/1"
            }
        };

        var expectedResponse = new ProjectClosuresSearchResponse
        {
            ProjectClosures = expectedClosures,
            TotalCount = expectedClosures.Count
        };

        var query = new GetProjectClosureQuery(projectRecordId);

        _projectClosureServiceMock
            .Setup(service => service.GetProjectClosuresByProjectRecordId(projectRecordId))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(1);
        result.ProjectClosures?.First()?.ShortProjectTitle.ShouldBe(expectedResponse?.ProjectClosures?.First()?.ShortProjectTitle);
        result.ProjectClosures?.First()?.Status.ShouldBe(expectedResponse?.ProjectClosures?.First()?.Status);
        _projectClosureServiceMock.Verify(service => service.GetProjectClosuresByProjectRecordId(projectRecordId), Times.Once);
    }
}