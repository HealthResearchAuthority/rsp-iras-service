using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ReviewBodyControllersTests;

public class GetUserReviewBodiesByIdsTests : TestServiceBase<ReviewBodyController>
{
    [Theory]
    [AutoData]
    public async Task GetUserReviewBodiesByIds_ShouldSendCommand(List<Guid> ids, List<ReviewBodyUserDto> expectedUsers)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<GetReviewBodyUserByIdsCommand>(c => c.Ids.SequenceEqual(ids)),
                default))
            .ReturnsAsync(expectedUsers);

        // Act
        var result = await Sut.GetUserReviewBodiesByIds(ids);

        // Assert
        mockMediator.Verify(
            m => m.Send(
                It.Is<GetReviewBodyUserByIdsCommand>(c => c.Ids.SequenceEqual(ids)),
                default),
            Times.Once);

        result.ShouldBe(expectedUsers);
    }
}