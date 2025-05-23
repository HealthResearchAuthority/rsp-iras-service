using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ReviewBodyControllersTests;

public class EnableReviewBodyTests : TestServiceBase
{
    private readonly ReviewBodyController _controller;

    public EnableReviewBodyTests()
    {
        _controller = Mocker.CreateInstance<ReviewBodyController>();
    }

    [Theory]
    [AutoData]
    public async Task Enable_ShouldSendCommand(Guid id)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();

        // Act
        await _controller.Enable(id);

        // Assert
        mockMediator.Verify(
            m => m.Send(It.Is<EnableReviewBodyCommand>(c => c.ReviewBodyId == id), default), Times.Once);
    }

    [Theory, AutoData]
    public async Task Enable_ShouldSendCommand_WhenReviewBodyIsEnabled(Guid id, ReviewBodyDto reviewBodyDto)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();

        // Mock mediator to return a ReviewBodyDto when the command is sent
        mockMediator
            .Setup(m => m.Send(It.IsAny<EnableReviewBodyCommand>(), default))
            .ReturnsAsync(reviewBodyDto);

        // Act
        var result = await _controller.Enable(id);

        // Assert mediator was called
        mockMediator.Verify(
            m => m.Send(It.Is<EnableReviewBodyCommand>(c => c.ReviewBodyId == id), default),
            Times.Once);

        result.ShouldBe(reviewBodyDto);
    }
}