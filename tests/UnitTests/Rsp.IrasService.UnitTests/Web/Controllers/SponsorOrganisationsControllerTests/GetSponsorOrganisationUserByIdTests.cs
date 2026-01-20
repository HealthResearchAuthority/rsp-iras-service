using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.SponsorOrganisationsControllerTests;

public class GetSponsorOrganisationUserById : TestServiceBase<SponsorOrganisationsController>
{
    [Theory]
    [AutoData]
    public async Task GetSponsorOrganisationUserById_ShouldSendQuery(
        Guid sponsorOrganisationUserId,
        SponsorOrganisationUserDto expected)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<GetSponsorOrganisationUserByIdCommand>(c => c.SponsorOrganisationUserId == sponsorOrganisationUserId),
                default))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.GetSponsorOrganisationUserById(sponsorOrganisationUserId);

        // Assert
        mockMediator.Verify(
            m => m.Send(
                It.Is<GetSponsorOrganisationUserByIdCommand>(c => c.SponsorOrganisationUserId == sponsorOrganisationUserId),
                default),
            Times.Once);

        result.Value.ShouldBe(expected);
    }
}