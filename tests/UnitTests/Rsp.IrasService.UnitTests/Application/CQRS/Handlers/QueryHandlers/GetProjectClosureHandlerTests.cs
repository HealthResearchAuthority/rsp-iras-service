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
        var expectedResponse = new ProjectClosureResponse
        {
            Id = "123",
            ShortProjectTitle = "Sample Project",
            Status = "With sponsor"
        };

        var query = new GetProjectClosureQuery(projectRecordId);

        _projectClosureServiceMock
            .Setup(service => service.GetProjectClosure(projectRecordId))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(expectedResponse.Id);
        result.ShortProjectTitle.ShouldBe(expectedResponse.ShortProjectTitle);
        result.Status.ShouldBe(expectedResponse.Status);

        _projectClosureServiceMock.Verify(service => service.GetProjectClosure(projectRecordId), Times.Once);
    }
}