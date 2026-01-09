using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectClosureControllerTests;

public class UpdateProjectClosureStatusTests : TestServiceBase<ProjectClosureController>
{
    [Theory, AutoData]
    public async Task UpdateProjectClosureStatus_Sends_Command(string projectRecordId, string status)
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();
        mediator
            .Setup(m => m.Send(It.IsAny<UpdateProjectClosureStatusCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var userId = "test-user-id";
        var httpContext = new DefaultHttpContext();
        httpContext.User = new System.Security.Claims.ClaimsPrincipal(
            new System.Security.Claims.ClaimsIdentity(new[]
            {
                new System.Security.Claims.Claim("userId", userId)
            })
        );

        Sut.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        // Act
        await Sut.UpdateProjectClosureStatus(projectRecordId, status);

        // Assert
        mediator.Verify(m => m.Send(
            It.Is<UpdateProjectClosureStatusCommand>(c =>
                c.ProjectRecordId == projectRecordId &&
                c.Status == status &&
                c.UserId == userId),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }
}