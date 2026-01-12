using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.RespondentControllerTests;

public class GetModificationParticipatingOrganisationAnswers : TestServiceBase<RespondentController>
{
    [Theory, AutoData]
    public async Task GetModificationParticipatingOrganisationAnswers_ByOrganisationId_ReturnsExpected
    (
        Guid participatingOrganisationId,
        ModificationParticipatingOrganisationAnswerDto expected
    )
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<GetModificationParticipatingOrganisationAnswerQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.GetModificationParticipatingOrganisationAnswer(participatingOrganisationId);

        // Assert
        result.ShouldBeEquivalentTo(expected);

        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<GetModificationParticipatingOrganisationAnswerQuery>
                    (q =>
                        q.ModificationParticipatingOrganisationId == participatingOrganisationId
                    ), It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}