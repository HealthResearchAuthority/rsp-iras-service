using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.SponsorOrganisationsControllerTests;

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