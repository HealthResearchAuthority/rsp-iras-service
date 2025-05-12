using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ReviewBodyControllersTests;

public class GetReviewBodiesTests : TestServiceBase
{
    private readonly ReviewBodyController _controller;

    public GetReviewBodiesTests()
    {
        _controller = Mocker.CreateInstance<ReviewBodyController>();
    }

    [Fact]
    public async Task GetReviewBodies_ShouldSendQuery()
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();

        // Act
        await _controller.GetAllReviewBodies(1, 100, null);

        // Assert
        mockMediator.Verify(
            m => m.Send(It.IsAny<GetReviewBodiesQuery>(), default), Times.Once);
    }
}