using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ReviewBodyControllersTests;

public class AddReviewBodyUserTests : TestServiceBase<ReviewBodyController>
{
    [Theory]
    [AutoData]
    public async Task AddUser_ShouldSendCommand(ReviewBodyUserDto reviewBodyUser)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<AddReviewBodyUserCommand>(c => c.ReviewBodyUserRequest == reviewBodyUser),
                default))
            .ReturnsAsync(reviewBodyUser);

        // Act
        var result = await Sut.AddUser(reviewBodyUser);

        // Assert
        mockMediator.Verify(
            m => m.Send(
                It.Is<AddReviewBodyUserCommand>(c => c.ReviewBodyUserRequest == reviewBodyUser),
                default),
            Times.Once);

        result.ShouldBe(reviewBodyUser);
    }
}