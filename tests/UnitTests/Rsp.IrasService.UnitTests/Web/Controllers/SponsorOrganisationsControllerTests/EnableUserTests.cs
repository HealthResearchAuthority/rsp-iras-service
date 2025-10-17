using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.SponsorOrganisationsControllerTests;
public class EnableUserTests : TestServiceBase<SponsorOrganisationsController>
{
    [Theory]
    [AutoData]
    public async Task EnableUser_ShouldSendCommand(
        string rtsId,
        Guid userId,
        SponsorOrganisationUserDto expected)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<EnableSponsorOrganisationUserCommand>(c => c.RtsId == rtsId && c.UserId == userId),
                default))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.EnableUser(rtsId, userId);

        // Assert
        mockMediator.Verify(
            m => m.Send(
                It.Is<EnableSponsorOrganisationUserCommand>(c => c.RtsId == rtsId && c.UserId == userId),
                default),
            Times.Once);

        result.Value.ShouldBe(expected);
    }
}