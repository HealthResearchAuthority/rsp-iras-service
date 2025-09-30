using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.RespondentControllerTests;

public class SaveModificationChangeAnswers : TestServiceBase<RespondentController>
{
    [Theory, AutoData]
    public async Task SaveModificationChangeAnswers_SendsCommand(ModificationChangeAnswersRequest request)
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<SaveModificationChangeAnswersCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await Sut.SaveModificationChangeAnswers(request);

        // Assert
        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<SaveModificationChangeAnswersCommand>(cmd => cmd.ModificationAnswersRequest == request),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}