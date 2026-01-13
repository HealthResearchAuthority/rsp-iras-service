using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.SponsorOrganisationsControllerTests;

public class EnableSponsorOrganisationTests : TestServiceBase<SponsorOrganisationsController>
{
    [Theory]
    [AutoData]
    public async Task EnableSponsorOrganisation_ShouldSendCommand(
        string rtsId,
        Guid userId,
        SponsorOrganisationDto expected)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<EnableSponsorOrganisationCommand>(c => c.RtsId == rtsId),
                default))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.EnableSponsorOrganisation(rtsId);

        // Assert
        mockMediator.Verify(
            m => m.Send(
                It.Is<EnableSponsorOrganisationCommand>(c => c.RtsId == rtsId),
                default),
            Times.Once);

        result.Value.ShouldBe(expected);
    }
}