using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.SponsorOrganisationsControllerTests;

public class GetAuditsForSponsorOrganisationTests : TestServiceBase<SponsorOrganisationsController>
{
    [Theory]
    [AutoData]
    public async Task GetGetAuditsForSponsorOrganisation_ShouldSendQuery(
        string rtsId,
        Guid userId, SponsorOrganisationAuditTrailResponse expected)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<GetAuditForSponsorOrganisationCommand>(c => c.RtsId == rtsId ),
                default))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.GetAuditsForSponsorOrganisation(rtsId);

        // Assert
        mockMediator.Verify(
            m => m.Send(
                It.Is<GetAuditForSponsorOrganisationCommand>(c => c.RtsId == rtsId),
                default),
            Times.Once);

        result.Value.ShouldBe(expected);
    }
}