using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ReviewBodyControllersTests
{
    public class UpdateReviewBodyTests : TestServiceBase
    {
        private readonly ReviewBodyController _controller;

        public UpdateReviewBodyTests()
        {
            _controller = Mocker.CreateInstance<ReviewBodyController>();
        }

        [Theory]
        [AutoData]
        public async Task Update_ShouldSendCommand(ReviewBodyDto reviewBodyDto)
        {
            // Arrange
            var mockMediator = Mocker.GetMock<IMediator>();

            // Act
            await _controller.Update(reviewBodyDto);

            // Assert
            mockMediator.Verify(
                m => m.Send(It.Is<UpdateReviewBodyCommand>(c => c.UpdateReviewBodyRequest == reviewBodyDto), default),
                Times.Once);
        }
    }
}