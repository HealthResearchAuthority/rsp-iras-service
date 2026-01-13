using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.RespondentControllerTests;

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