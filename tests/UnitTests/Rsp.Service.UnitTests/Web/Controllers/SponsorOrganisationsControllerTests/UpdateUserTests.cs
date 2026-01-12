using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.SponsorOrganisationsControllerTests;

public class UpdateUserTests : TestServiceBase<SponsorOrganisationsController>
{
    [Theory]
    [AutoData]
    public async Task UpdateUser_ShouldSendCommand(
        string rtsId,
        Guid userId,
        SponsorOrganisationUserDto existingUser)
    {
        // Arrange

        existingUser.RtsId = rtsId;
        existingUser.UserId = userId;

        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<UpdateSponsorOrganisationUserCommand>(c => c.User.RtsId == rtsId && c.User.UserId == userId),
                default))
            .ReturnsAsync(existingUser);

        existingUser.IsAuthoriser = false;
        existingUser.SponsorRole = "TestRole";

        // Act
        var result = await Sut.UpdateUser(existingUser);

        // Assert
        mockMediator.Verify(
            m => m.Send(
                It.Is<UpdateSponsorOrganisationUserCommand>(c => c.User.RtsId == rtsId && c.User.UserId == userId),
                default),
            Times.Once);

        result.Value.ShouldNotBeNull();
        result.Value.SponsorRole.ShouldBe("TestRole");
        result.Value.IsAuthoriser.ShouldBeFalse();
    }
}