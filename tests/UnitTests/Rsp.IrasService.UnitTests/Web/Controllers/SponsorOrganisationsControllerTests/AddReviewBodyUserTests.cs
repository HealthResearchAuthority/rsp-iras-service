using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.SponsorOrganisationsControllerTests;

public class AddSponsorOrganisationUserTests : TestServiceBase<SponsorOrganisationsController>
{
    [Theory]
    [AutoData]
    public async Task AddUser_ShouldSendCommand(SponsorOrganisationUserDto sponsorOrganisationUser)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<AddSponsorOrganisationUserCommand>(c =>
                    c.SponsorOrganisationUserRequest == sponsorOrganisationUser),
                default))
            .ReturnsAsync(sponsorOrganisationUser);

        // Act
        var result = await Sut.AddUser(sponsorOrganisationUser);

        // Assert
        mockMediator.Verify(
            m => m.Send(
                It.Is<AddSponsorOrganisationUserCommand>(c =>
                    c.SponsorOrganisationUserRequest == sponsorOrganisationUser),
                default),
            Times.Once);

        result.ShouldBe(sponsorOrganisationUser);
    }
}