using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetReviewBodiesHandlerTests
{
    private readonly Mock<IReviewBodyService> _reviewBodyServiceMock;
    private readonly GetReviewBodiesHandler _handler;

    public GetReviewBodiesHandlerTests()
    {
        _reviewBodyServiceMock = new Mock<IReviewBodyService>();
        _handler = new GetReviewBodiesHandler(_reviewBodyServiceMock.Object);
    }

    [Fact]
    public async Task Handle_GetApplicationsQuery_ShouldReturnListOfApplications()
    {
        // Arrange
        var expectedResponse = new IrasService.Application.DTOS.Responses.AllReviewBodiesResponse
        {
            ReviewBodies = new List<ReviewBodyDto>
                {
                    new() { OrganisationName = "App-123", Description = "Approved" },
                    new() { OrganisationName = "App-456", Description = "Pending" }
                },
            TotalCount = 2
        };

        var query = new GetReviewBodiesQuery(1, 100, null);

        _reviewBodyServiceMock
            .Setup(service => service.GetReviewBodies(1, 100, null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(expectedResponse.TotalCount);

        _reviewBodyServiceMock.Verify(service => service.GetReviewBodies(1, 100, null), Times.Once);
    }
}