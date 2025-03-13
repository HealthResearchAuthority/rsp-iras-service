using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ReviewBodyControllersTests;

public class DisableTests : TestServiceBase
{
    private readonly ReviewBodyController _controller;

    public DisableTests()
    {
        _controller = Mocker.CreateInstance<ReviewBodyController>();
    }

    [Theory]
    [AutoData]
    public async Task Disable_ShouldSendCommand(Guid request)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();

        // Act
        await _controller.Disable(request);

        // Assert
        mockMediator.Verify(
            m => m.Send(It.Is<DisableReviewBodyCommand>(c => c.ReviewBodyId == request), default), Times.Once);
    }
}