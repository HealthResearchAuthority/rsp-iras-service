﻿using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ReviewBodyControllersTests;

public class RemoveReviewBodyUserTests : TestServiceBase<ReviewBodyController>
{
    [Theory, AutoData]
    public async Task RemoveReviewBodyUser_ShouldSendCommand(Guid reviewBodyId, Guid userId)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();

        // Act
        await Sut.RemoveUser(reviewBodyId, userId);

        // Assert
        mockMediator.Verify(
            m => m.Send(It.Is<RemoveReviewBodyUserCommand>(c => c.ReviewBodyId == reviewBodyId && c.UserId == userId), default), Times.Once);
    }
}