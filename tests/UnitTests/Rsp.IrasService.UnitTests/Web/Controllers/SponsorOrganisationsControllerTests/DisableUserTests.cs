﻿using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.SponsorOrganisationsControllerTests;

public class DisableUserTests : TestServiceBase<SponsorOrganisationsController>
{
    [Theory]
    [AutoData]
    public async Task DisableUser_ShouldSendCommand(
        string rtsId,
        Guid userId,
        SponsorOrganisationUserDto expected)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<DisableSponsorOrganisationUserCommand>(c => c.RtsId == rtsId && c.UserId == userId),
                default))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.DisableUser(rtsId, userId);

        // Assert
        mockMediator.Verify(
            m => m.Send(
                It.Is<DisableSponsorOrganisationUserCommand>(c => c.RtsId == rtsId && c.UserId == userId),
                default),
            Times.Once);

        result.Value.ShouldBe(expected);
    }
}