using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Handlers.QueryHandlers;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.QueryHandlers;

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
                    new() { RegulatoryBodyName = "App-123", Description = "Approved" },
                    new() { RegulatoryBodyName = "App-456", Description = "Pending" }
                },
            TotalCount = 2
        };

        var query = new GetReviewBodiesQuery(1, 100, nameof(ReviewBodyDto.RegulatoryBodyName), "asc", null);

        _reviewBodyServiceMock
            .Setup(service => service.GetReviewBodies(1, 100, nameof(ReviewBodyDto.RegulatoryBodyName), "asc", null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(expectedResponse.TotalCount);

        _reviewBodyServiceMock.Verify(service => service.GetReviewBodies(1, 100, nameof(ReviewBodyDto.RegulatoryBodyName), "asc", null), Times.Once);
    }
}