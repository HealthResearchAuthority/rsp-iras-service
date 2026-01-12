using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.RespondentControllerTests;

public class SaveModificationParticipatingOrganisations : TestServiceBase<RespondentController>
{
    [Theory, AutoData]
    public async Task SaveModificationParticipatingOrganisations_SendsCommand(List<ModificationParticipatingOrganisationDto> request)
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<SaveModificationParticipatingOrganisationsCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await Sut.SaveModificationParticipatingOrganisations(request);

        // Assert
        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<SaveModificationParticipatingOrganisationsCommand>(cmd => cmd.ModificationParticipatingOrganisationRequest == request),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}