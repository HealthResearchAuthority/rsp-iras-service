using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.RespondentControllerTests;

public class SaveModificationParticipatingOrganisationAnswers : TestServiceBase<RespondentController>
{
    [Theory, AutoData]
    public async Task SaveModificationParticipatingOrganisationAnswers_SendsCommand(ModificationParticipatingOrganisationAnswerDto request)
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<SaveModificationParticipatingOrganisationAnswersCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await Sut.SaveModificationParticipatingOrganisationAnswers(request);

        // Assert
        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<SaveModificationParticipatingOrganisationAnswersCommand>(cmd => cmd.ModificationParticipatingOrganisationAnswerRequest == request),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}