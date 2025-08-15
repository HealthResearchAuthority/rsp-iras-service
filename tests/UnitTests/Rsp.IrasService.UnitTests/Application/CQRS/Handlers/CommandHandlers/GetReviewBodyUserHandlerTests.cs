using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class GetReviewBodyUserHandlerTests
{
    private readonly GetReviewBodyUserHandler _handler;
    private readonly Mock<IReviewBodyService> _reviewBodyServiceMock;

    public GetReviewBodyUserHandlerTests()
    {
        _reviewBodyServiceMock = new Mock<IReviewBodyService>();
        _handler = new GetReviewBodyUserHandler(_reviewBodyServiceMock.Object);
    }

    [Theory]
    [AutoData]
    public async Task Handle_Should_Call_Service_And_Return_Result(Guid userId, List<ReviewBodyUserDto> expected)
    {
        // Arrange
        _reviewBodyServiceMock
            .Setup(s => s.GetRegulatoryBodiesUsersByUserId(userId))
            .ReturnsAsync(expected);

        var request = new GetReviewBodyUserCommand(userId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.ShouldBeSameAs(expected);
        _reviewBodyServiceMock.Verify(s => s.GetRegulatoryBodiesUsersByUserId(userId), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Handle_Should_Return_Empty_List_When_Service_Returns_Empty(Guid userId)
    {
        // Arrange
        var expected = new List<ReviewBodyUserDto>();
        _reviewBodyServiceMock
            .Setup(s => s.GetRegulatoryBodiesUsersByUserId(userId))
            .ReturnsAsync(expected);

        var request = new GetReviewBodyUserCommand(userId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeEmpty();
        _reviewBodyServiceMock.Verify(s => s.GetRegulatoryBodiesUsersByUserId(userId), Times.Once);
    }
}