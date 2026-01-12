using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.RespondentControllerTests;

public class SaveRespondentAnswersTests : TestServiceBase
{
    private readonly RespondentController _controller;

    public SaveRespondentAnswersTests()
    {
        _controller = Mocker.CreateInstance<RespondentController>();
    }

    [Theory]
    [AutoData]
    public async Task SaveRespondentAnswers_ShouldSendCommand(RespondentAnswersRequest request)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();

        // Act
        await _controller.SaveRespondentAnswers(request);

        // Assert
        mockMediator.Verify(
            m => m.Send(It.Is<SaveResponsesCommand>(c => c.RespondentAnswersRequest == request), default), Times.Once);
    }
}