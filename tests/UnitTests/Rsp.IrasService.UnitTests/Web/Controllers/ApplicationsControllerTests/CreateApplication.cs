using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ApplicationsControllerTests;

public class CreateApplication : TestServiceBase<ApplicationsController>
{
    [Theory, AutoData]
    public async Task CreateApplication_ShouldSendCommand_AndReturnCreatedApplication(ApplicationRequest request, ApplicationResponse mockResponse, string userId)
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
        request.UserId = userId;

        mockMediator
            .Setup(m => m.Send(It.Is<CreateApplicationCommand>(c => c.CreateApplicationRequest == request), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await Sut.CreateApplication(request);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(mockResponse);

        // Verify
        mockMediator.Verify
        (
            m => m
            .Send
            (
                It.Is<CreateApplicationCommand>(c => c.CreateApplicationRequest == request),
                default
            ),
            Times.Once
        );
    }
}