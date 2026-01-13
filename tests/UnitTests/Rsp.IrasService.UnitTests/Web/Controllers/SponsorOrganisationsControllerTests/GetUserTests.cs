using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.SponsorOrganisationsControllerTests;

public class GetUserTests : TestServiceBase<SponsorOrganisationsController>
{
    [Theory]
    [AutoData]
    public async Task GetUser_ShouldSendQuery(
        string rtsId,
        Guid userId,
        SponsorOrganisationUserDto expected)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<GetSponsorOrganisationUserCommand>(c => c.RtsId == rtsId && c.UserId == userId),
                default))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.GetUser(rtsId, userId);

        // Assert
        mockMediator.Verify(
            m => m.Send(
                It.Is<GetSponsorOrganisationUserCommand>(c => c.RtsId == rtsId && c.UserId == userId),
                default),
            Times.Once);

        result.Value.ShouldBe(expected);
    }
}