using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ReviewBodyControllersTests;

public class GetUserReviewBodiesTests : TestServiceBase<ReviewBodyController>
{
    [Theory]
    [AutoData]
    public async Task GetUserReviewBodies_ShouldSendCommand(Guid userId)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<GetReviewBodyUserCommand>(c => c.UserId == userId),
                default))
            .ReturnsAsync(new List<ReviewBodyUserDto>());

        // Act
        await Sut.GetUserReviewBodies(userId);

        // Assert
        mockMediator.Verify(
            m => m.Send(It.Is<GetReviewBodyUserCommand>(c => c.UserId == userId), default),
            Times.Once);
    }
}