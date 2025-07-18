using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.RespondentControllerTests;

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