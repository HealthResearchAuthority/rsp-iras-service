using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.SponsorOrganisationsControllerTests;

public class GetAllActiveSponsorOrganisationsForEnabledUserTests : TestServiceBase<SponsorOrganisationsController>
{
    [Theory]
    [AutoData]
    public async Task GetAllActiveSponsorOrganisationsForEnabledUser_ShouldSendCommand(
        Guid userId,
        IEnumerable<SponsorOrganisationDto> expected)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<GetAllActiveSponsorOrganisationsForEnabledUserCommand>(c => c.UserId == userId),
                default))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.GetAllActiveSponsorOrganisationsForEnabledUser(userId);

        // Assert
        mockMediator.Verify(
            m => m.Send(
                It.Is<GetAllActiveSponsorOrganisationsForEnabledUserCommand>(c => c.UserId == userId),
                default),
            Times.Once);

        result.ShouldBe(expected);
    }
}