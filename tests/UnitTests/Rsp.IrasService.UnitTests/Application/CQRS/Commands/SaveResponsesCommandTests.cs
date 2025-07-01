using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Commands
{
    namespace Rsp.IrasService.UnitTests.Application.CQRS.Commands
    {
        public class SaveResponsesCommandTests
        {
            private readonly Mock<IMediator> _mediatorMock;

            public SaveResponsesCommandTests()
            {
                _mediatorMock = new Mock<IMediator>();
            }

            [Fact]
            public async Task Send_SaveResponsesCommand_ShouldCallMediatorOnce()
            {
                // Arrange
                var request = new RespondentAnswersRequest
                {
                    Id = "R-123",
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

                _mediatorMock
                    .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask); // Since it returns void

                // Act
                await _mediatorMock.Object.Send(command, CancellationToken.None);

                // Assert
                _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
            }
        }
    }
}