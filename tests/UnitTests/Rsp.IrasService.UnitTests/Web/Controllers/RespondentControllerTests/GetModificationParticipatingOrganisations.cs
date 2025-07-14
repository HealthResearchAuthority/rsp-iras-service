using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.RespondentControllerTests;

public class GetModificationParticipatingOrganisations : TestServiceBase<RespondentController>
{
    [Theory, AutoData]
    public async Task GetModificationParticipatingOrganisations_ByModificationChangeIdAndProjectRecordId_ReturnsExpected
    (
        Guid modificationChangeId,
        string projectRecordId,
        List<ModificationParticipatingOrganisationDto> expected
    )
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<GetModificationParticipatingOrganisationsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.GetModificationParticipatingOrganisations(modificationChangeId, projectRecordId);

        // Assert
        result.ShouldBeEquivalentTo(expected);

        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<GetModificationParticipatingOrganisationsQuery>
                    (q =>
                        q.ProjectModificationChangeId == modificationChangeId &&
                        q.ProjectRecordId == projectRecordId
                    ), It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}