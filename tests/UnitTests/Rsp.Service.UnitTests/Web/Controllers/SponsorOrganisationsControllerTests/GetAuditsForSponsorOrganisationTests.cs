using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.SponsorOrganisationsControllerTests;

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