using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ApplicationsControllerTests;

public class ProjectRecordStatusUpdateTests : TestServiceBase<ApplicationsController>
{
    [Theory, AutoData]
    public async Task CreateApplication_ShouldSendCommand_AndReturnCreatedApplication(string userId, UpdateProjectRecordStatusCommand request)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();

        // Set the User on the controller
        var claims = new List<Claim> { new("userId", userId) };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        Sut.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(identity) }
        };

        // Assign UserId to request as controller would do
        var command = new UpdateProjectRecordStatusCommand
        {
            ProjectRecordId = "PR1",
            Status = "With sponsor"
        };

        mockMediator
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = Sut.UpdateProjectRecordStatus("PR1", "With sponsor");

        // Assert
        result.ShouldNotBeNull();
    }
}