using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectClosureControllerTests;

public class CreateProjectClosureTests : TestServiceBase<ProjectClosureController>
{
    [Theory, AutoData]
    public async Task CreateProjectClosure_ShouldSendCommand_AndReturnProjectClosure(ProjectClosureRequest request, ProjectClosureResponse mockResponse, string userId)
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
            .Setup(m => m.Send(It.Is<CreateProjectClosureCommand>(c => c.CreateProjectClosureRequest == request), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await Sut.CreateProjectClosure(request);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(mockResponse);

        // Verify
        mockMediator.Verify
        (
            m => m
            .Send
            (
                It.Is<CreateProjectClosureCommand>(c => c.CreateProjectClosureRequest == request),
                default
            ),
            Times.Once
        );
    }

    [Theory]
    [AutoData]
    public async Task GetProjectClosure_ShouldReturnOk_WhenProjectRecordExists(string projecrRecordId,
        ProjectClosureResponse mockResponse)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<GetProjectClosureQuery>(q => q.ProjectRecordId == projecrRecordId), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await Sut.GetProjectClosureById(projecrRecordId);

        // Assert
        result.Value.ShouldBe(mockResponse);

        // Verify
        mockMediator.Verify
        (
            m => m
            .Send
            (
                It.Is<GetProjectClosureQuery>(c => c.ProjectRecordId == projecrRecordId),
                default
            ),
            Times.Once
        );
    }
}