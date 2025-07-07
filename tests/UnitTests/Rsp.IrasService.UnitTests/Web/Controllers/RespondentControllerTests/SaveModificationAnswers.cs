using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.RespondentControllerTests;

public class SaveModificationAnswers : TestServiceBase<RespondentController>
{
    [Theory, AutoData]
    public async Task SaveModificationAnswers_SendsCommand(ModificationAnswersRequest request)
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<SaveModificationAnswersCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await Sut.SaveModificationAnswers(request);

        // Assert
        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<SaveModificationAnswersCommand>(cmd => cmd.ModificationAnswersRequest == request),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}