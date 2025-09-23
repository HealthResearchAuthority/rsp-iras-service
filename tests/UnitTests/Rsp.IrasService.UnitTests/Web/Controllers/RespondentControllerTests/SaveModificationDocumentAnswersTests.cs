using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.RespondentControllerTests;

public class SaveModificationDocumentAnswersTests : TestServiceBase<RespondentController>
{
    [Theory, AutoData]
    public async Task SaveModificationDocumentAnswers_SendsCommand(List<ModificationDocumentAnswerDto> request)
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<SaveModificationDocumentAnswersCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await Sut.SaveModificationDocumentAnswers(request);

        // Assert
        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<SaveModificationDocumentAnswersCommand>(cmd => cmd.ModificationDocumentAnswerRequest == request),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}