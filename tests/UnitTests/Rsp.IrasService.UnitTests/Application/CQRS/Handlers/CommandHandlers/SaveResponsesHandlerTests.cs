using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class SaveResponsesHandlerTests
{
    private readonly SaveResponsesHandler _handler;
    private readonly Mock<IRespondentService> _respondentServiceMock;

    public SaveResponsesHandlerTests()
    {
        _respondentServiceMock = new Mock<IRespondentService>();
        _handler = new SaveResponsesHandler(_respondentServiceMock.Object);
    }

    [Fact]
    public async Task Handle_SaveResponsesCommand_ShouldCallServiceOnce()
    {
        // Arrange
        var request = new RespondentAnswersRequest
        {
            ProjectApplicationRespondentId = "R-123",
            RespondentAnswers = new List<RespondentAnswerDto>
            {
                new()
                {
                    QuestionId = "Q1",
                    CategoryId = "C1",
                    SectionId = "S1",
                    AnswerText = "Yes",
                    OptionType = "Single",
                    SelectedOption = "Yes",
                    Answers = new List<string> { "Yes" }
                },
                new()
                {
                    QuestionId = "Q2",
                    CategoryId = "C2",
                    SectionId = "S2",
                    AnswerText = "No",
                    OptionType = "Multiple",
                    SelectedOption = "No",
                    Answers = new List<string> { "No", "Maybe" }
                }
            }
        };

        var command = new SaveResponsesCommand(request);

        _respondentServiceMock
            .Setup(service => service.SaveResponses(request))
            .Returns(Task.CompletedTask); // Since it returns void

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _respondentServiceMock.Verify(service => service.SaveResponses(request), Times.Once);
    }
}