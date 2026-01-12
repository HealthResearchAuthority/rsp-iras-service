using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.CQRS.Handlers.CommandHandlers;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class CreateReviewBodyHandlerTests
{
    private readonly CreateReviewBodyHandler _handler;
    private readonly Mock<IReviewBodyService> _reviewBodyServiceMock;

    public CreateReviewBodyHandlerTests()
    {
        _reviewBodyServiceMock = new Mock<IReviewBodyService>();
        _handler = new CreateReviewBodyHandler(_reviewBodyServiceMock.Object);
    }

    [Fact]
    public async Task Handle_GetApplicationsQuery_ShouldReturnListOfApplications()
    {
        // Arrange
        var request = new ReviewBodyDto
        {
            RegulatoryBodyName = "App-123",
            Description = "Approved"
        };

        var expectedResponse = new ReviewBodyDto
        {
            RegulatoryBodyName = "App-123",
            Description = "Approved"
        };

        var query = new CreateReviewBodyCommand(request);

        _reviewBodyServiceMock
            .Setup(service => service.CreateReviewBody(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();

        _reviewBodyServiceMock.Verify(service => service.CreateReviewBody(It.IsAny<ReviewBodyDto>()), Times.Once);
    }
}