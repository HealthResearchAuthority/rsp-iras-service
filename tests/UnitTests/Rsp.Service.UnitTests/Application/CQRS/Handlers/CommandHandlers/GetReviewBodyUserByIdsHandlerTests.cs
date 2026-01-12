using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.CQRS.Handlers.CommandHandlers;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class GetReviewBodyUserByIdsHandlerTests
{
    private readonly GetReviewBodyUserByIdsHandler _handler;
    private readonly Mock<IReviewBodyService> _reviewBodyServiceMock;

    public GetReviewBodyUserByIdsHandlerTests()
    {
        _reviewBodyServiceMock = new Mock<IReviewBodyService>();
        _handler = new GetReviewBodyUserByIdsHandler(_reviewBodyServiceMock.Object);
    }

    [Theory]
    [AutoData]
    public async Task Handle_Should_Call_Service_And_Return_Result(List<Guid> ids, List<ReviewBodyUserDto> expected)
    {
        // Arrange
        _reviewBodyServiceMock
            .Setup(s => s.GetRegulatoryBodiesUsersByIds(ids))
            .ReturnsAsync(expected);

        var request = new GetReviewBodyUserByIdsCommand(ids);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.ShouldBeSameAs(expected);
        _reviewBodyServiceMock.Verify(s => s.GetRegulatoryBodiesUsersByIds(ids), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Handle_Should_Return_Empty_List_When_Service_Returns_Empty(List<Guid> ids)
    {
        // Arrange
        var expected = new List<ReviewBodyUserDto>();
        _reviewBodyServiceMock
            .Setup(s => s.GetRegulatoryBodiesUsersByIds(ids))
            .ReturnsAsync(expected);

        var request = new GetReviewBodyUserByIdsCommand(ids);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeEmpty();
        _reviewBodyServiceMock.Verify(s => s.GetRegulatoryBodiesUsersByIds(ids), Times.Once);
    }
}