using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ReviewBodyControllersTests;

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

    [Theory, AutoData]
    public async Task Disable_ShouldSendCommand_WhenReviewBodyIsDisabled(Guid id, ReviewBodyDto reviewBodyDto)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();

        // Mock mediator to return a ReviewBodyDto when the command is sent
        mockMediator
            .Setup(m => m.Send(It.IsAny<DisableReviewBodyCommand>(), default))
            .ReturnsAsync(reviewBodyDto);

        // Act
        var result = await _controller.Disable(id);

        // Assert mediator was called
        mockMediator.Verify(
            m => m.Send(It.Is<DisableReviewBodyCommand>(c => c.ReviewBodyId == id), default),
            Times.Once);

        result.ShouldBe(reviewBodyDto);
    }
}