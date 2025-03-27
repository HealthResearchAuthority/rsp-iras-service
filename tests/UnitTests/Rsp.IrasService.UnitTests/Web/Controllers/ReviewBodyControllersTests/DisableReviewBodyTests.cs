using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ReviewBodyControllersTests;

public class DisableReviewBodyTests : TestServiceBase
{
    private readonly ReviewBodyController _controller;

    public DisableReviewBodyTests()
    {
        _controller = Mocker.CreateInstance<ReviewBodyController>();
    }

    [Theory]
    [AutoData]
    public async Task Disable_ShouldSendCommand(Guid id)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();

        // Act
        await _controller.Disable(id);

        // Assert
        mockMediator.Verify(
            m => m.Send(It.Is<DisableReviewBodyCommand>(c => c.ReviewBodyId == id), default), Times.Once);
    }
}