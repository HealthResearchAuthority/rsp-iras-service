using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.RespondentControllerTests;

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