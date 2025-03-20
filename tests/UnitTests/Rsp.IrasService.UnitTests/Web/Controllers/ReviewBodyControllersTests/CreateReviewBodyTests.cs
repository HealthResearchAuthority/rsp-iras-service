using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ReviewBodyControllersTests
{
    public class CreateReviewBodyTests : TestServiceBase
    {
        private readonly ReviewBodyController _controller;

        public CreateReviewBodyTests()
        {
            _controller = Mocker.CreateInstance<ReviewBodyController>();
        }

        [Theory]
        [AutoData]
        public async Task Create_ShouldSendCommand(ReviewBodyDto reviewBodyDto)
        {
            // Arrange
            var mockMediator = Mocker.GetMock<IMediator>();

            // Act
            await _controller.Create(reviewBodyDto);

            // Assert
            mockMediator.Verify(
                m => m.Send(It.Is<CreateReviewBodyCommand>(c => c.CreateReviewBodyRequest == reviewBodyDto), default),
                Times.Once);
        }
    }
}