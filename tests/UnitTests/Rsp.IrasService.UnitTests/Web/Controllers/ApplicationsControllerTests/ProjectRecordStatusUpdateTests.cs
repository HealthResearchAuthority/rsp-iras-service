using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ApplicationsControllerTests;

public class ProjectRecordStatusUpdateTests : TestServiceBase<ApplicationsController>
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
            .Setup(m => m.Send(It.Is<UpdateProjectRecordStatusCommand>(c => c.UpdateApplicationRequest == request), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await Sut.UpdateProjectRecordStatus(request);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(mockResponse);

        // Verify
        mockMediator.Verify
        (
            m => m
            .Send
            (
                It.Is<UpdateProjectRecordStatusCommand>(c => c.UpdateApplicationRequest == request),
                default
            ),
            Times.Once
        );
    }
}